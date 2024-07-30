

namespace Order_Management.Service.Dto
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		//public DateTime OrderDate { get; set; }
		public ICollection<OrderItemDto> OrderItems { get; set; }
		public string? PaymentMethod { get; set; }
		public string? ClientSecret { get; set; }
		public string Status { get; set; }
	}
}
