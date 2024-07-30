using Microsoft.EntityFrameworkCore;
using Order_Management.Repository.Data;
using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;

namespace Order_Management.Repository.Repo_Implementation
{
	public class OrderRepository:GenericRepository<Order>,IOrderRepository
	{
		private new readonly OrderManagementDbContext _dbContext;
        public OrderRepository(OrderManagementDbContext dbContext):base(dbContext)
        {
            _dbContext= dbContext;
        }

		public async Task<IReadOnlyList<Order>> GetAllOrdersWithInvoicesAsync()
		{
			return await _dbContext.Orders
			.Include(o => o.Invoice)
			.ToListAsync();
		}

		public async Task<Order?> GetOrderByIdWithInvoiceAsync(int orderId)
		{
			return await _dbContext.Orders
				.Include(o => o.Invoice)
				.FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(int customerId)
		{
			return await _dbContext.Orders
				.Where(o => o.CustomerId == customerId)
				.Include(o => o.OrderItems)
				.ToListAsync();
		}
	}
}
