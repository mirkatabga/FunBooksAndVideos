namespace FunBooksAndVideos.Domain
{
    public class PhysicalProduct : Product
    {
        public PhysicalProduct(
            Guid id,
            string name,
            string description,
            decimal price,
            ProductType productType,
            int quantity,
            string size)
            : base(id, name, description, price, productType)
        {
            Quantity = quantity;
            Size = size;
        }

        public int Quantity { get; private set; }
        public string Size { get; private set; }

        public void ReduceQuantity(int count)
        {
            if(Quantity < count)
            {
                throw new ArgumentException($"{nameof(count)} should be greater or equal to {nameof(Quantity)}");
            }

            Quantity -= count;
        }
    }
}