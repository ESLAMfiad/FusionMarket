using Order_Management.Core.Repositories;
using Order_Management.Core.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Order_Management.Repository.Repo_Implementation
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}

		public async Task<CustomerBasket?> GetBasketAsync(string CustomerBasketId)
		{
			var basket = await _database.StringGetAsync(CustomerBasketId);

			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var created = JsonSerializer.Serialize(basket);

			var createdorupdated = await _database.StringSetAsync(basket.CustomerBasketId,created, TimeSpan.FromDays(30));

			if (!createdorupdated) return null;

			return await GetBasketAsync(basket.CustomerBasketId);
		}

		public async Task<bool> DeleteBasketAsync(string CustomerBasketId)
		{
			return await _database.KeyDeleteAsync(CustomerBasketId);
		}
	}
}
