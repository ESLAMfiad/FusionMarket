using Order_Management.Core.Entities;
using System.Text.Json;

namespace Order_Management.Repository.Data
{
	public static class OrderContextSeed
	{
		public static async Task SeedAsync(OrderManagementDbContext dbContext)
		{

			if (!dbContext.Products.Any())
			{
				var productsData = File.ReadAllText("../Order Managment.Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);
				if (products?.Count > 0)
				{
					foreach (var product in products)
					{
						await dbContext.Set<Product>().AddAsync(product);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
