using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class Order : EntityBase
    {
        public Order(
            Guid id,
            decimal totalPrice,
            Guid customerId,
            string? deliveryAddress = null)
        {
            Id = id;
            TotalPrice = totalPrice;
            CustomerId = customerId;
            DeliveryAddress = deliveryAddress;
        }

        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public string? DeliveryAddress { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; } = new HashSet<OrderItem>();
    }
}