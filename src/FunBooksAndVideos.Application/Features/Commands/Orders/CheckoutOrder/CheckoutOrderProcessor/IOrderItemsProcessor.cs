using FunBooksAndVideos.Application.Models.Orders;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{

    public interface IOrderItemsProcessor
    {
        Task<ProcessOrderItemsResponse> ProcessAsync(CheckoutOrderCommand command);
    }
}