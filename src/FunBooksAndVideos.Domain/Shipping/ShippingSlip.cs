using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class ShippingSlip : EntityBase
    {
        public ShippingSlip(
            Guid id,
            Guid customerId,
            Guid purchaseOrderId,
            string billTo,
            string shipTo)
        {
            Id = id;
            CustomerId = customerId;
            PurchaseOrderId = purchaseOrderId;
            BillTo = billTo;
            ShipTo = shipTo;
        }

        public Guid CustomerId { get; private set; }
        public Guid PurchaseOrderId { get; private set; }
        public string BillTo { get; private set; }
        public string ShipTo { get; private set; }
        public ICollection<ShippingSlipItem> ShippingSlipItems { get; private set; } = new HashSet<ShippingSlipItem>();

        public void AddSlipItem(ShippingSlipItem item)
        {
            ShippingSlipItems.Add(item);
        }
    }
}