using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Core.Entities;
using Order_Management.Errors;
using Order_Management.Service.implementation;
using Order_Managment.Service.Dto;

namespace Order_Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketService _basketService;
		private readonly IMapper _mapper;

		public BasketController(IBasketService basketService, IMapper mapper)
		{
			_basketService = basketService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string BasketId)
		{
			var basket = await _basketService.GetBasketAsync(BasketId);

			return basket is null? new CustomerBasket(BasketId):basket;
		}
		
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(customerBasketDto basket)
		{
			var mappedBasket= _mapper.Map<customerBasketDto,CustomerBasket>(basket);
			var updatedBasket = await _basketService.UpdateBasketAsync(mappedBasket);
			if (updatedBasket == null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(updatedBasket);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string CustomerBasketId)
		{
			return await _basketService.DeleteBasketAsync(CustomerBasketId);
			
		}
	}
}
