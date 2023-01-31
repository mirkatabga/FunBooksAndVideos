using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}