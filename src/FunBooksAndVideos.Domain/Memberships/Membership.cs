using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class Membership : EntityBase
    {
        public Membership(
            Guid id,
            string name,
            string description,
            decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}