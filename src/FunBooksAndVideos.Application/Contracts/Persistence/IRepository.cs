using FunBooksAndVideos.Domain.Common;

namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : EntityBase
	{
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T?> GetByIdAsync(int id);

		void Add(T entity);

		void Update(T entity);

		void Delete(T entity);
	}
}