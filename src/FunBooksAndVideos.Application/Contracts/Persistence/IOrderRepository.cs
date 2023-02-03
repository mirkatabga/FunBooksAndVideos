using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByIdAsync(Guid id, params string[] includes);
    }
}