using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }
    }
}