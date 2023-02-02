using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICheckoutOrderCommandConsistencyValidator _consistencyValidator;

        public CheckoutOrderCommandHandler(
            IUnitOfWork unitOfWork,
            ICheckoutOrderCommandConsistencyValidator consistencyValidator)
        {
            _uow = unitOfWork;
            _consistencyValidator = consistencyValidator;
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            await _consistencyValidator.ValidateAsync(request);

            var productIds = request.GetProductIdsForOrder();
            var products = _uow.Products.GetByIds(productIds);

            var membershipId = request.GetMembershipsIdsForOrder().SingleOrDefault();
            var membership = await _uow.Memberships.GetByIdAsync(membershipId);

            var order = MapOrder(request, products, membership);
            _uow.Orders.Add(order);

            UpdateCustomerPurchases(order.CustomerId, products, membership);

            _uow.SaveChanges();

            return order.Id;
        }

        private void UpdateCustomerPurchases(Guid customerId, ICollection<Product> products, Membership? membership)
        {
            var customer = _uow.Customers.GetById(
                            customerId,
                            nameof(Customer.Membership),
                            nameof(Customer.Products));

            customer!.UpdateMembership(membership);

            var productsToAdd = products
                .Where(p => !customer.Products.Any(cp => cp.Id == p.Id))
                .ToList();

            customer.AddProducts(productsToAdd);
            _uow.Customers.Update(customer);
        }

        private static Order MapOrder(CheckoutOrderCommand request, ICollection<Product> products, Membership? membership)
        {
            var order = new Order(
                id: Guid.NewGuid(),
                totalPrice: CalculateTotalPrice(request, products, membership),
                customerId: request.CustomerId,
                deliveryAddress: request.DeliveryAddress
            );

            foreach (var item in request.OrderItems)
            {
                var orderItem = new OrderItem(
                    id: Guid.NewGuid(),
                    name: item!.Name!,
                    membershipId: item.MembershipId,
                    productId: item.ProductId,
                    quantity: item.Quantity
                );

                order.OrderItems.Add(orderItem);
            }

            return order;
        }

        private static decimal CalculateTotalPrice(
            CheckoutOrderCommand request,
            ICollection<Product> products,
            Membership? membership)
        {
            decimal total = membership?.Price ?? 0;

            foreach (var item in request.OrderItems)
            {
                if (item.ProductId != null)
                {
                    var price = products.Single(p => p.Id == item.ProductId).Price;
                    total += price * (item.Quantity ?? 1);
                }
            }

            return total;
        }
    }
}