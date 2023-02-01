using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IProductRepository : IRepository<Product>
    {
        ICollection<Product> GetByIds(IEnumerable<Guid> productIds);
    }
}