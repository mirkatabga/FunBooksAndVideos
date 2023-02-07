using FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor;
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

        public bool HasPhysicalProduct { get; private set; }

        public void AggregateItemsResponses(ICollection<ProcessOrderItemsResponse> processorsResponses)
        {
            foreach (var response in processorsResponses)
            {
                foreach (var item in response.OrderItems)
                {
                    Order.AddOrderItem(item);
                }

                HasPhysicalProduct |= response.HasPhysicalProduct;
            }
        }
    }
}