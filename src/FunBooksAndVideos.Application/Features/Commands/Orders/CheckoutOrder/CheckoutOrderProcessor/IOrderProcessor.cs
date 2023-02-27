using FunBooksAndVideos.Application.Models.Orders;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{
    public interface IOrderProcessor
    {
        Task<ProcessOrderResponse> ProcessAsync(CheckoutOrderCommand command);
    }
}