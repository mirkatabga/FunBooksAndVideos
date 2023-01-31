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
    }
}
