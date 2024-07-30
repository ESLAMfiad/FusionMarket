

using Orders_Management.Core.Specifications;

namespace Order_Management.Core.Repositories
{
	public interface IGenericRepository<T> where T : class 
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificationcs<T> spec);
		Task<T> GetByIdWithSpecAsync(ISpecificationcs<T> spec);

		Task<int> CountAsync(ISpecificationcs<T> spec);
	}
	
}
