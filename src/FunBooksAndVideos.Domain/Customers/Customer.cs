using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Domain
{
    public class Customer : EntityBase
    {
        public Customer(
            Guid id,
            string firstName,
            string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid? MembershipId { get; private set; }
        public Membership? Membership { get; private set; }
        public ICollection<Product> Products { get; private set; } = new HashSet<Product>();

        public void AddProducts(ICollection<Product> productsToAdd)
        {
            foreach (var product in productsToAdd)
            {
                Products.Add(product);
            }
        }

        public void UpdateMembership(Membership? membership)
        {
            MembershipId = membership?.Id;
            Membership = membership;
        }

        public void UpdateProducts(ICollection<Product> products) => Products = products;
    }
}
