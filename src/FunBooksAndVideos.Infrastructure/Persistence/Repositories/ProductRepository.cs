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

        public async Task<ICollection<Product>> GetByIdsAsync(IEnumerable<Guid> productIds)
        {
            if (productIds?.Any() == true)
            {
                return await _dbContext.Products!
                    .AsQueryable<Product>()
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync();
            }

            return new List<Product>();
        }
    }
}