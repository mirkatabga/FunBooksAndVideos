using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Application.Models.Orders;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly IEnumerable<IOrderItemsProcessor> _processors;

        public OrderProcessor(
            IUnitOfWork unitOfWork,
            IEnumerable<IOrderItemsProcessor> processors)
        {
            _uow = unitOfWork;
            _processors = processors;
        }

        public async Task<ProcessOrderResponse> ProcessAsync(CheckoutOrderCommand command)
        {
            var order = new Order(
                id: Guid.NewGuid(),
                customerId: command.CustomerId,
                deliveryAddress: command.DeliveryAddress);

            var response = new ProcessOrderResponse(order);

            foreach (var processor in _processors)
            {
                response = await processor.ProcessAsync(command, response);
            }

            await _uow.Orders.AddAsync(order);
            await _uow.SaveChangesAsync();

            return response;
        }
    }
}