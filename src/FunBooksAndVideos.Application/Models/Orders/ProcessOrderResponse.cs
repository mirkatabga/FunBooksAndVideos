using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Models.Orders
{
    public class ProcessOrderResponse
    {
        public ProcessOrderResponse(Order order)
        {
            Order = order;
        }

        public Order Order { get; }

        public bool HasPhysicalProduct { get; set; }
    }
}