
using System.Text.Json.Serialization;

namespace Order_Management.Core.Entities
{
	public class Order
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount {  get; set; }

		[JsonIgnore]
		public ICollection<OrderItem> OrderItems { get; set; } =new List<OrderItem>();
		public Invoice Invoice { get; set; }
		public string? PaymentMethod { get; set; }
		public string Status {  get; set; }
		[JsonIgnore]
		public Customer Customer { get; set; }
		public string? ClientSecret { get; set; } 

	}
}
