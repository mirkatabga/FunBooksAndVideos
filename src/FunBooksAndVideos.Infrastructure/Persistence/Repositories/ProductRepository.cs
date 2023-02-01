using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }
    }
}