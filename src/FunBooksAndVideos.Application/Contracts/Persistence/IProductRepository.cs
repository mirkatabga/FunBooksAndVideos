using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetByIdsAsync(IEnumerable<Guid> productIds);
    }
}