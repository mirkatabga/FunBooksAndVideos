
namespace FunBooksAndVideos.Domain
{
    public class OrderItem
    {
        public OrderItem(
            Guid id,
            string name,
            Guid? membershipId,
            Guid? productId,
            bool isPhysical,
            int? quantity)
        {
            Id = id;
            Name = name;
            MembershipId = membershipId;
            ProductId = productId;
            IsPhysical = isPhysical;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid? MembershipId { get; private set; }
        public Guid? ProductId { get; private set; }
        public bool IsPhysical { get; private set; }
        public int? Quantity { get; private set; }
    }
}