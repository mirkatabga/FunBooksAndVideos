using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Exceptions;
using FunBooksAndVideos.Application.Models;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandConsistencyValidator : ICheckoutOrderCommandConsistencyValidator
    {
        private readonly IUnitOfWork _uow;

        public CheckoutOrderCommandConsistencyValidator(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task ValidateAsync(CheckoutOrderCommand request)
        {
            var customer = await _uow.Customers.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            var productIds = request.GetProductIdsForOrder();
            var products = _uow.Products.GetByIds(productIds);

            CheckForMissingProducts(productIds, products);
            await ValidateMembershipAsync(request, customer);
            ValidateOrderItems(request.OrderItems, products);
            ValidateDeliveryAddress(request, products);
        }

        private static void ValidateDeliveryAddress(CheckoutOrderCommand request, ICollection<Product> products)
        {
            var containsPhysicalItem = products?.Any(p => p is PhysicalProduct) == true;

            if (containsPhysicalItem && string.IsNullOrWhiteSpace(request.DeliveryAddress))
            {
                throw new ValidationException(
                    nameof(request.DeliveryAddress), "Required when order contains physical item(s)");
            }
        }

        private static void ValidateOrderItems(ICollection<OrderItemDto> orderItems, ICollection<Product> products)
        {
            foreach (var item in orderItems)
            {
                if(item is null)
                {
                    throw new ValidationException(
                         nameof(OrderItem), "Cannot be null");
                }

                if (item.MembershipId is null && item.ProductId is null)
                {
                    throw new ValidationException(
                        nameof(OrderItem), $"{nameof(item.MembershipId)} or {nameof(item.ProductId)} should have value");
                }

                else if (item.MembershipId is not null && item.ProductId is not null)
                {
                    throw new ValidationException(
                         nameof(OrderItem), $"Only one of {nameof(item.MembershipId)} and {nameof(item.ProductId)} should have value");
                }

                else if (item.ProductId is not null)
                {
                    var product = products.Single(p => p.Id == item.ProductId);

                    if (product is PhysicalProduct physicalProduct)
                    {
                        if (item.Quantity is null || item.Quantity < 1)
                        {
                            throw new ValidationException(nameof(OrderItem.Quantity),
                                "Quantity should have a valid value for physical products.");
                        }

                        if (item.Quantity > physicalProduct.Quantity)
                        {
                            throw new ValidationException(
                                nameof(OrderItem), $"Product with id {item.ProductId} does not have enough items to fulfill the order.");
                        }
                    }
                }
            }
        }

        private async Task ValidateMembershipAsync(CheckoutOrderCommand request, Customer customer)
        {
            var membershipIds = request.GetMembershipsIdsForOrder();

            if (membershipIds.Any())
            {
                if (membershipIds.Count > 1)
                {
                    throw new ValidationException(nameof(Membership), "Only one membership can be purchased per order.");
                }

                var membershipId = membershipIds.Single();
                var membership = await _uow.Memberships.GetByIdAsync(membershipId);

                if (membership is null)
                {
                    throw new NotFoundException(nameof(Membership), membershipId);
                }

                if (customer.MembershipId == membership!.Id)
                {
                    throw new ValidationException(nameof(Membership), $"Customer already owns membership with id: {membership.Id}.");
                }
            }
        }

        private static void CheckForMissingProducts(ICollection<Guid> productIds, ICollection<Product> products)
        {
            if (productIds.Any())
            {
                var missingIds = GetMissingEntitiesIds(products, productIds);

                if (missingIds.Any())
                {
                    throw new NotFoundException(nameof(products), missingIds);
                }
            }
        }

        private static ICollection<Guid> GetMissingEntitiesIds(ICollection<Product> products, IEnumerable<Guid> ids)
        {
            if (products.Count == ids.Count())
            {
                return new List<Guid>();
            }

            ICollection<Guid> missing = new List<Guid>();

            bool isFound = false;

            foreach (var id in ids)
            {
                foreach (var product in products)
                {
                    if (id == product.Id)
                    {
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    missing.Add(id);
                }
            }

            return missing;
        }
    }
}