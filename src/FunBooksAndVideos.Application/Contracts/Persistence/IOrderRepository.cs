using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IOrderRepository : IRepository<Order>
    {
        new Task<Order?> GetByIdAsync(Guid id);
    }
}