﻿

namespace Order_Management.Service.Dto
{
	public class InvoiceDto
	{
		public int InvoiceId { get; set; }
		public int OrderId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public decimal TotalAmount { get; set; }
	}
}