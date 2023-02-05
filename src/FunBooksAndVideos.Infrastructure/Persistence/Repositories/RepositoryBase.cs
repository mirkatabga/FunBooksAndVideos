using Microsoft.EntityFrameworkCore;
using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly FunBooksAndVideosContext _dbContext;

        public RepositoryBase(FunBooksAndVideosContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            await Task.CompletedTask;
        }
    }
}