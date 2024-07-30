using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Errors;
using Order_Management.Service.Dto;
using Order_Managment.Service.Dto;
using Order_Managment.Service.Interfaces;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(ApiResponse), 201)]
		[ProducesResponseType(typeof(ApiResponse), 400)]
		[ProducesResponseType(typeof(ApiResponse), 500)]
		public async Task<IActionResult> CreateOrder(OrderReqDto orderReqDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ApiResponse(400, "Invalid model state"));
			}

			try
			{
				var order = await _orderService.CreateOrderAsync(orderReqDto);
				return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, 
					new ApiResponse(201, "Order created successfully"));

			}
			catch 
			{
				return StatusCode(500, new ApiResponse(500, "An error occurred while creating the order"));
			}
		}

		[HttpGet("{orderId}")]
		[ProducesResponseType(typeof(OrderDto), 200)]
		[ProducesResponseType(typeof(ApiResponse), 404)]
		public async Task<IActionResult> GetOrderById(int orderId)
		{
			var order = await _orderService.GetOrderByIdAsync(orderId);
			if (order == null)
			{
				return NotFound(new ApiResponse(404,"order not found"));
			}
			return Ok(order);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(IReadOnlyList<OrderDto>), 200)]
		[ProducesResponseType(typeof(ApiResponse), 401)]
		[ProducesResponseType(typeof(ApiResponse), 403)]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _orderService.GetAllOrdersAsync();
			return Ok(orders);
		}

		[HttpPut("{orderId}/status")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse), 204)]
		[ProducesResponseType(typeof(ApiResponse), 400)]
		[ProducesResponseType(typeof(ApiResponse), 404)]
		public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] StatusDto request)
		{
			if (request == null || string.IsNullOrEmpty(request.Status))
			{
				return BadRequest(new ApiResponse(400,"Invalid request payload."));
			}
			var order = await _orderService.GetOrderByIdAsync(orderId);
			if (order == null)
			{
				return NotFound(new ApiResponse(404, "Order not found"));
			}

			await _orderService.UpdateOrderStatusAsync(orderId, request.Status);
			return NoContent();
		}
	}
}
