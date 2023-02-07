using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor
{
    public class ProcessOrderItemsResponse
    {
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public bool HasPhysicalProduct { get; internal set; }
    }
}