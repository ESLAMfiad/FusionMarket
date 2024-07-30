using Microsoft.EntityFrameworkCore;
using Order_Management.Repository.Data;
using Order_Management.Core.Repositories;
using Orders_Management.Core.Specifications;

namespace Order_Management.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly OrderManagementDbContext _dbContext;

		public GenericRepository(OrderManagementDbContext dbContext) //ask clr for creating obj from dbcontext implicitly
		{
			_dbContext = dbContext;
		}
		#region Without Specifications
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
		#endregion
		public async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}
		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecificationcs<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}
		public async Task<T> GetByIdWithSpecAsync(ISpecificationcs<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync(); 
		}

		public async Task<int> CountAsync(ISpecificationcs<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecificationcs<T> spec)
		{
			return SpecEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
		}
	}
}
