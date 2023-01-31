using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class Product : EntityBase
    {
        public  Product(
            Guid id,
            string name,
            string description,
            decimal price,
            ProductType productType)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ProductType = productType;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public ProductType ProductType { get; private set; }
    }
}