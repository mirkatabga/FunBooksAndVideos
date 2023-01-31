using FunBooksAndVideos.Domain;
using FunBooksAndVideos.Infrastructure.Persistence;
using Ordering.Application.Contracts.Persistence;

namespace FunBooksAndVideos.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }
    }
}