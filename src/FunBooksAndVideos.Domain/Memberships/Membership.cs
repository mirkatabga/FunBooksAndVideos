namespace FunBooksAndVideos.Domain
{
    public class Membership
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

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}