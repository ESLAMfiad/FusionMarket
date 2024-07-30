using AutoMapper;
using Microsoft.Extensions.Configuration;
using Order_Management.Core.Entities;
using Order_Management.Service.Dto;
using Order_Managment.Service.Interfaces;
using Orders_Managment.Core;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.implementation
{
	public class StripePaymentService : IStripePaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public StripePaymentService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_configuration = configuration;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		
		public async Task<OrderDto?> CreateOrUpdatePaymentIntent(int orderId)
		{
			//secret key
			StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
			var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
			if (order is null)
			{
				// Log error
				Console.WriteLine($"Order with ID {orderId} not found.");
				return null;
			}

			var subtotal = order.OrderItems.Sum(item => item.UnitPrice * item.Quantity);

			//create payment intent
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(order.PaymentMethod))
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)subtotal * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string> { "card" }
				};
				paymentIntent = await service.CreateAsync(options);
				order.PaymentMethod = paymentIntent.Id;
				order.ClientSecret = paymentIntent.ClientSecret; 
			}
			else //update
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)subtotal * 100,
				};
				paymentIntent = await service.UpdateAsync(order.PaymentMethod, options);
				order.ClientSecret = paymentIntent.ClientSecret;
			}
			await _unitOfWork.Repository<Order>().UpdateAsync(order);
			await _unitOfWork.CompleteAsync();

			var orderDto = _mapper.Map<OrderDto>(order);
			return orderDto;
		}
	}
}
