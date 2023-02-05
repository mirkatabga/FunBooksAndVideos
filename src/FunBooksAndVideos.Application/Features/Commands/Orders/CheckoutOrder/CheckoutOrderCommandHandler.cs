
using AutoMapper;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Features.Events.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, OrderVm>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICheckoutOrderCommandConsistencyValidator _consistencyValidator;
        private readonly IPublisher _publisher;

        public CheckoutOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICheckoutOrderCommandConsistencyValidator consistencyValidator,
            IPublisher publisher)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _consistencyValidator = consistencyValidator;
            _publisher = publisher;
        }

        public async Task<OrderVm> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            await _consistencyValidator.ValidateAsync(request);

            var productIds = request.GetProductIdsForOrder();
            var products = await _uow.Products.GetByIdsAsync(productIds);

            var membershipId = request.GetMembershipsIdsForOrder().SingleOrDefault();
            var membership = await _uow.Memberships.GetByIdAsync(membershipId);

            var order = MapOrder(request, products, membership);
            await _uow.Orders.AddAsync(order);

            UpdateCustomerPurchases(order.CustomerId, products, membership);

            await _uow.SaveChangesAsync();

            if (products.OfType<PhysicalProduct>().Any())
            {
                await _publisher.Publish(new PhysicalProductsOrderedEvent(order.Id), cancellationToken);
            }

            return _mapper.Map<OrderVm>(order);
        }

        private async Task UpdateCustomerPurchases(Guid customerId, ICollection<Product> products, Membership? membership)
        {
            var customer = await _uow.Customers.GetByIdAsync(
                            customerId,
                            nameof(Customer.Membership),
                            nameof(Customer.Products));

            customer!.UpdateMembership(membership);

            var productsToAdd = products
                .Where(p => !customer.Products.Any(cp => cp.Id == p.Id))
                .ToList();

            customer.AddProducts(productsToAdd);
            await _uow.Customers.UpdateAsync(customer);
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