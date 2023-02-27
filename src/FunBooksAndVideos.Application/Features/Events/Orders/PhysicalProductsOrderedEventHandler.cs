using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FunBooksAndVideos.Application.Features.Events.Orders
{
    public class PhysicalProductsOrderedEventHandler : INotificationHandler<PhysicalProductsOrderedEvent>
    {
        private readonly ILogger<PhysicalProductsOrderedEventHandler> _logger;
        private readonly IUnitOfWork _uow;

        public PhysicalProductsOrderedEventHandler(
            ILogger<PhysicalProductsOrderedEventHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _uow = unitOfWork;
        }

        public async Task Handle(PhysicalProductsOrderedEvent notification, CancellationToken cancellationToken)
        {
            var order = await _uow.Orders.GetByIdAsync(
                notification.OrderId,
                nameof(Order.Customer),
                $"{nameof(Order.OrderItems)}.Product");

            _logger.LogTrace("Start creation of shipping slip for order with id: {OrderId}", order!.Id);

            var shippingSlip = new ShippingSlip(
                id: Guid.NewGuid(),
                customerId: order.CustomerId,
                purchaseOrderId: order.Id,
                billTo: order.DeliveryAddress!,
                shipTo: order.DeliveryAddress!
            );

            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem.Product is PhysicalProduct physicalProduct)
                {
                    shippingSlip.AddSlipItem(new ShippingSlipItem(
                        id: Guid.NewGuid(),
                        description: orderItem.Name,
                        orderedQuantity: orderItem.Quantity,
                        shippedQuantity: orderItem.Quantity,
                        productId: orderItem.ProductId!.Value,
                        price: orderItem.Product.Price));

                    physicalProduct.ReduceQuantity(orderItem.Quantity);
                    await _uow.Products.UpdateAsync(physicalProduct);
                }
            }

            await _uow.ShippingSlips.AddAsync(shippingSlip);
            await _uow.SaveChangesAsync();

             _logger.LogTrace("Shipping slip created with id: {OrderId}", shippingSlip.Id);
        }
    }
}