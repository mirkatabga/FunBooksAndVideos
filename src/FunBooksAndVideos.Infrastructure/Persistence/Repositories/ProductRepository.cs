using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Product> GetByIds(IEnumerable<Guid> productIds)
        {
            if (productIds?.Any() == true)
            {
                return _dbContext.Products!
                    .AsQueryable<Product>()
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();
            }

            return new List<Product>();
        }
    }
}