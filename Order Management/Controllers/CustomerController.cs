using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Core.Repositories;
using Order_Management.Core.Entities;
using Order_Management.Dto;
using Order_Management.Service;
using AutoMapper;
using Order_Managment.Service.Interfaces;
using Order_Management.Errors;
using Order_Management.Service.Dto;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		protected readonly ICustomerService _customerRepo;
		private readonly IMapper _mapper;

		public CustomerController(ICustomerService customerRepo, IMapper mapper)
		{
			_customerRepo = customerRepo;
			_mapper = mapper;
		}


		//[HttpPost]
		//public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
		//{
		//	try
		//	{
		//		var createdCustomer = await _customerRepo.CreateCustomerAsync(customerDto);
		//		return Ok(createdCustomer);
		//	}
		//	catch
		//	{
		//		return BadRequest(new ApiResponse(400));
		//	}
		//}


		[HttpGet("{customerId}/orders")]
		public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetCustomerOrders(int customerId)
		{
			try
			{
				var orders = await _customerRepo.GetCustomerOrdersAsync(customerId);
				return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
			}
			catch
			{
				return BadRequest(new ApiResponse(400));
			}
		}
	}
}
