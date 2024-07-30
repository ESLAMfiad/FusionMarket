using Order_Management.Core.Repositories;
using Order_Management.Repository;
using Order_Management.Repository.Data;
using Order_Management.Repository.Repo_Implementation;
using Orders_Managment.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OrderManagementDbContext _dbContext;
		private IOrderRepository _orderRepository;
		private Hashtable _repositories;

		public UnitOfWork(OrderManagementDbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new Hashtable();
		}
		public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);


		public Task<int> CompleteAsync()
		{
			return _dbContext.SaveChangesAsync();
		}

		public ValueTask DisposeAsync()
		{
			return _dbContext.DisposeAsync();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			var type= typeof(TEntity).Name; //entity name 
			if (!_repositories.ContainsKey(type))
			{
				var repository= new GenericRepository<TEntity>(_dbContext);
				_repositories.Add(type, repository);
			}
			return (GenericRepository<TEntity>) _repositories[type];
		}
	}
}
