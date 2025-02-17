﻿using Order_Management.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Interfaces
{
	public interface IStripePaymentService
	{
		Task<OrderDto?> CreateOrUpdatePaymentIntent(int orderId);
	}
}
