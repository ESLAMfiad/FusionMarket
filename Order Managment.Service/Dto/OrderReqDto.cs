using Order_Management.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Dto
{
	public class OrderReqDto
	{
		public int CustomerId { get; set; }
		public ICollection<OrderItemDto> OrderItems { get; set; }
		//public string? PaymentMethod { get; set; }  // Optional, can be set internally
	}
}
