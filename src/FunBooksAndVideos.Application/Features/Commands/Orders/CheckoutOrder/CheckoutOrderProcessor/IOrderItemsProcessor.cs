using FunBooksAndVideos.Application.Models.Orders;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{

    public interface IOrderItemsProcessor
    {
        Task<ProcessOrderResponse> ProcessAsync(CheckoutOrderCommand command, ProcessOrderResponse response);
    }
}