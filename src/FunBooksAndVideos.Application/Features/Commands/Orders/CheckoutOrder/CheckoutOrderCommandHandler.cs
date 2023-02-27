using AutoMapper;
using FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor;
using FunBooksAndVideos.Application.Features.Events.Orders;
using FunBooksAndVideos.Application.Models.Orders;
using MediatR;

namespace FunBooksAndVideos.Application.Features.Commands.Orders
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, OrderVm>
    {
        private readonly IMapper _mapper;
        private readonly ICheckoutOrderCommandConsistencyValidator _consistencyValidator;
        private readonly IPublisher _publisher;
        private readonly IOrderProcessor _orderProcessor;

        public CheckoutOrderCommandHandler(
            IMapper mapper,
            ICheckoutOrderCommandConsistencyValidator consistencyValidator,
            IPublisher publisher,
            IOrderProcessor orderProcessor)
        {
            _mapper = mapper;
            _consistencyValidator = consistencyValidator;
            _publisher = publisher;
            _orderProcessor = orderProcessor;
        }

        public async Task<OrderVm> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            await _consistencyValidator.ValidateAsync(request);

            var response = await _orderProcessor.ProcessAsync(request);

            if (response.HasPhysicalProduct)
            {
                await _publisher.Publish(new PhysicalProductsOrderedEvent(response.Order.Id), cancellationToken);
            }

            return _mapper.Map<OrderVm>(response.Order);
        }
    }
}