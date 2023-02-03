using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class ShippingSlipItem : EntityBase
    {
        public ShippingSlipItem(
            Guid id,
            string description,
            int orderedQuantity,
            int shippedQuantity,
            Guid productId,
            decimal price)
        {
            Id = id;
            Description = description;
            OrderedQuantity = orderedQuantity;
            ShippedQuantity = shippedQuantity;
            ProductId = productId;
            Price = price;
        }

        public string Description { get; private set; }
        public int OrderedQuantity { get; private set; }
        public int ShippedQuantity { get; private set; }
        public Guid? ShippingSlipId { get; private set; }
        public ShippingSlip? ShippingSlip { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
    }
}