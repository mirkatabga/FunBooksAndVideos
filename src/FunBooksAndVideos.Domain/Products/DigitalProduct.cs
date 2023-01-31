namespace FunBooksAndVideos.Domain
{
    public class DigitalProduct : Product
    {
        public DigitalProduct(
            Guid id,
            string name,
            string description,
            decimal price,
            ProductType productType,
            double sizeInMB)
            : base(id, name, description, price, productType)
        {
            SizeInMB = sizeInMB;
        }

        public double SizeInMB { get; private set; }
    }
}