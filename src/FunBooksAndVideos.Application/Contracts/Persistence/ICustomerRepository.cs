using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer? GetById(Guid id, params string[] includes);
    }
}