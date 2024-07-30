using AutoMapper;
using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;
using Order_Management.Repository.Repo_Implementation;
using Order_Management.Service.Dto;
using Order_Managment.Service.Dto;
using Order_Managment.Service.implementation;
using Order_Managment.Service.Interfaces;
using Orders_Managment.Core;


namespace Order_Management.Service.implementation
{
    public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IStripePaymentService _stripePaymentService;
		public OrderService(IMapper mapper, IEmailService emailService, IUnitOfWork unitOfWork, IStripePaymentService stripePaymentService)
		{
			_mapper = mapper;
			_emailService = emailService;
			_unitOfWork = unitOfWork;
			_stripePaymentService = stripePaymentService;
		}
		public async Task<OrderResponseDto> CreateOrderAsync(OrderReqDto orderReqDto)
		{
			try
			{
				var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(orderReqDto.CustomerId);
				if (customer == null)
				{
					throw new InvalidOperationException($"Customer {orderReqDto.CustomerId} does not exist.");
				}

				var order = _mapper.Map<Order>(orderReqDto);
				order.OrderDate = DateTime.UtcNow;
				order.TotalAmount = 0;
				order.Customer = customer;
				order.Status = "Pending";

				foreach (var item in order.OrderItems)
				{
					var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
					if (product == null || product.Stock < item.Quantity)
					{
						throw new InvalidOperationException($"Product {item.ProductId} is out of stock or insufficient quantity.");
					}

					item.UnitPrice = product.Price;
					var totalAmountForItem = item.Quantity * item.UnitPrice;
					if (totalAmountForItem > 200)
					{
						item.Discount = 0.10m * totalAmountForItem;
					}
					else if (totalAmountForItem > 100)
					{
						item.Discount = 0.05m * totalAmountForItem;
					}

					order.TotalAmount += (item.Quantity * item.UnitPrice) - item.Discount;
				}


				// Save the order temporarily to get an Order ID
				await _unitOfWork.Repository<Order>().AddAsync(order);
				await _unitOfWork.CompleteAsync();

				// Create payment intent using StripePaymentService
				var paymentResult = await _stripePaymentService.CreateOrUpdatePaymentIntent(order.OrderId);

				if (paymentResult == null)
				{
					throw new InvalidOperationException("Payment processing failed.");
				}

				// Update order status to shipped
				order.Status = "Shipped";
				order.ClientSecret = paymentResult.ClientSecret;
				order.PaymentMethod = paymentResult.PaymentMethod;

				await _unitOfWork.Repository<Order>().UpdateAsync(order);

				// Update product stock
				foreach (var item in order.OrderItems)
				{
					var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
					product.Stock -= item.Quantity;
					await _unitOfWork.Repository<Product>().UpdateAsync(product);
				}

				// Generate invoice
				var invoice = new Invoice
				{
					OrderId = order.OrderId,
					InvoiceDate = DateTime.UtcNow,
					TotalAmount = order.TotalAmount
				};
				await _unitOfWork.Repository<Invoice>().AddAsync(invoice);

				await _unitOfWork.CompleteAsync();

				await _emailService.SendEmailAsync(order.Customer.Email, "Order Placed", "Your order has been placed successfully.");
				var orderResponseDto = _mapper.Map<OrderResponseDto>(order);

				return orderResponseDto;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error creating order: {ex.Message}");
				throw;
			}
		}

		public async Task<Order?> GetOrderByIdAsync(int orderId)
		{
			return await _unitOfWork.OrderRepository.GetOrderByIdWithInvoiceAsync(orderId);
		}

		public async Task<IEnumerable<Order>> GetAllOrdersAsync()
		{
			return await _unitOfWork.OrderRepository.GetAllOrdersWithInvoicesAsync();
		}

		public async Task UpdateOrderStatusAsync(int orderId, string status)
		{
			var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
			if (order != null)
			{
				order.Status = status;
				await _unitOfWork.Repository<Order>().UpdateAsync(order);
				await _emailService.SendEmailAsync(order.Customer.Email, "Order Status Updated", $"Your order status has been updated to {status}");
				await _unitOfWork.CompleteAsync();
			}
		}
	}
}
