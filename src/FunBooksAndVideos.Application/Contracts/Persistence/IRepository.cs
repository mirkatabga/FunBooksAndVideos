using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : EntityBase
	{
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T?> GetByIdAsync(int id);

		Task<T> AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(T entity);
	}
}