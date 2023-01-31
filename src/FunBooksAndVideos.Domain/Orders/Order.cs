namespace FunBooksAndVideos.Domain
{
    public class Order
    {
        public Order(
            Guid id,
            decimal totalPrice,
            Guid customerId,
            ICollection<OrderItem> orderItems,
            string? deliveryAddress = null)
        {
            Id = id;
            TotalPrice = totalPrice;
            CustomerId = customerId;
            OrderItems = orderItems;
            DeliveryAddress = deliveryAddress;
        }

        public Guid Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
        public string? DeliveryAddress { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; } = new HashSet<OrderItem>();
    }
}