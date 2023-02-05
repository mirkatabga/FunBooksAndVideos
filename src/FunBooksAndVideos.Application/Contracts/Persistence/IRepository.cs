using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T?> GetByIdAsync(Guid id, params string[] includes);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}