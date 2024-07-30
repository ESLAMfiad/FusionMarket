using AutoMapper;
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
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		//private readonly IMapper _mapper;

		public UserController(IUserService userService)
		{
			_userService = userService;
			
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserDto userDto)
		{
			try
			{
				var userResponse = await _userService.RegisterAsync(userDto);
				return Ok(userResponse);
			}
			catch (ArgumentException)
			{
				return BadRequest(new ApiResponse(400));
			}
			catch(Exception)
			{
				return BadRequest(new ApiResponse(500));
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserResponseDto>> Login([FromBody] UserLoginDto userLoginDto)
		{
			try
			{
				var userResponse = await _userService.LoginAsync(userLoginDto);
				return Ok(userResponse);
			}
			catch
			{
				return Unauthorized(new ApiResponse(401));
			}
		}
	}
}
