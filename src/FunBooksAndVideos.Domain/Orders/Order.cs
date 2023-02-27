using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class Order : EntityBase
    {
        public Order(
            Guid id,
            Guid customerId,
            string? deliveryAddress = null)
        {
            Id = id;
            CustomerId = customerId;
            DeliveryAddress = deliveryAddress;
        }

        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public string? DeliveryAddress { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; } = new HashSet<OrderItem>();

        public void AddOrderItem(OrderItem orderItem)
        {
            if(orderItem is null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            TotalPrice += orderItem.Price * orderItem.Quantity;
            OrderItems.Add(orderItem);
        }
    }
}