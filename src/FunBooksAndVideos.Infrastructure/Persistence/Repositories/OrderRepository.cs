using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order?> GetByIdAsync(Guid id, params string[] includes)
        {
            var query = _dbContext.Orders!.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}