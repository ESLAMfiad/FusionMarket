using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;

namespace Order_Management.Service.implementation
{
	public interface IBasketService
	{
		Task<CustomerBasket?> GetBasketAsync(string CustomerBasketId);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
		Task<bool> DeleteBasketAsync(string CustomerBasketId);
	}
	public class BasketService : IBasketService
	{
		private readonly IBasketRepository _basketRepository;

		public BasketService(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		public async Task<CustomerBasket?> GetBasketAsync(string CustomerBasketId)
		{
			return await _basketRepository.GetBasketAsync(CustomerBasketId);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			return await _basketRepository.UpdateBasketAsync(basket);
		}

		public async Task<bool> DeleteBasketAsync(string CustomerBasketId)
		{
			return await _basketRepository.DeleteBasketAsync(CustomerBasketId);
		}
	}
}
