using Order_Management.Core.Entities;

namespace Order_Management.Core.Repositories
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string CustomerBasketId);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
		Task<bool> DeleteBasketAsync(string CustomerBasketId);
	}
}
	