using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class ShippingSlipItem : EntityBase
    {
        public ShippingSlipItem(
            Guid id,
            string description,
            int orderedQuantity,
            int shippedQuantity)
        {
            Id = id;
            Description = description;
            OrderedQuantity = orderedQuantity;
            ShippedQuantity = shippedQuantity;
        }

        public string Description { get; private set; }
        public int OrderedQuantity { get; private set; }
        public int ShippedQuantity { get; private set; }
        public Guid? ShippingSlipId { get; private set; }
        public ShippingSlip? ShippingSlip { get; private set; }
    }
}