using Order_Management.Core.Entities;


namespace Order_Management.Core.Repositories
{
	public interface IOrderRepository:IGenericRepository<Order>
	{
		Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(int customerId);
		Task<Order?> GetOrderByIdWithInvoiceAsync(int orderId);
		Task<IReadOnlyList<Order>> GetAllOrdersWithInvoicesAsync();


	}
}
