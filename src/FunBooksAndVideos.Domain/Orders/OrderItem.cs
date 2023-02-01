using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class OrderItem : EntityBase
    {
        public OrderItem(
            Guid id,
            string name,
            Guid? membershipId,
            Guid? productId,
            int? quantity)
        {
            Id = id;
            Name = name;
            MembershipId = membershipId;
            ProductId = productId;
            Quantity = quantity;
        }

        public string Name { get; private set; }
        public Guid? OrderId { get; private set; }
        public Order? Order {get; private set; }
        public Guid? MembershipId { get; private set; }
        public Membership? Membership { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int? Quantity { get; private set; }
    }
}