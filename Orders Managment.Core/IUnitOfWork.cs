using Order_Management.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders_Managment.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
		IOrderRepository OrderRepository { get; }

		Task<int> CompleteAsync();
	}
}
 