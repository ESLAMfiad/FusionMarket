
using System.Text.Json.Serialization;

namespace Order_Management.Core.Entities
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		//public string PictureUrl { get; set; }
		public int Stock { get; set; }

		[JsonIgnore]
		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
