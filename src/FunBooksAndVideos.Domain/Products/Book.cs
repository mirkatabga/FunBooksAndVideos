namespace FunBooksAndVideos.Domain
{
    public class Book : Product
    {
        public Book(
            Guid id,
            string name,
            string description,
            decimal price,
            int quantity,
            int pages,
            string author)
            : base(id, name, description, price)
        {
            Quantity = quantity;
            Pages = pages;
            Author = author;
        }

        public int Pages { get; private set; }
        public string Author { get; private set; }
        public int Quantity { get; private set; }
    }
}