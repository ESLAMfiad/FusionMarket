
using System.ComponentModel.DataAnnotations;

namespace Order_Management.Service.Dto
{
	public class OrderItemDto
	{
		[Required]
		public int ProductId { get; set; }
		[Required]
		[Range(1, int.MaxValue,ErrorMessage ="quantity must be greater than 0")]
		public int Quantity { get; set; }
		//[Required]
		//public int OrderItemId { get; set; }
		//[Required]
		//public int OrderId { get; set; }
		//[Required]
		//[Range(0.1,double.MaxValue,ErrorMessage ="price cannot be zero")]
		//public decimal UnitPrice { get; set; }
		//public decimal Discount { get; set; }
	}
}
