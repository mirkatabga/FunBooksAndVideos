using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }

        public Customer? GetById(Guid id, params string[] includes)
        {
            var query = _dbContext.Customers!.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(c => c.Id == id);
        }
    }
}