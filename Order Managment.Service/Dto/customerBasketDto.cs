using Order_Management.Service.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Dto
{
	public class customerBasketDto
	{
		[Required]
		public string customerBasketId {  get; set; }
		public List<OrderItemDto> Items {  get; set; }
	}
}
