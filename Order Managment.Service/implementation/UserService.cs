using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order_Management.Core.Entities;
using Order_Management.Service.Dto;
using Order_Management.Core.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Order_Managment.Service.Interfaces;
using Order_Managment.Service.Dto;


namespace Order_Management.Service.implementation
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService
		,RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_roleManager = roleManager;
		}
		private static bool IsValidRole(string role)
		{
			return role == "Admin" || role == "Customer";
		}

		public async Task<UserResponseDto> LoginAsync(UserLoginDto userLoginDto)
		{
			var user = await _userManager.FindByNameAsync(userLoginDto.Name);
			if (user == null)
			{
				throw new ArgumentException("Invalid username");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
			if (!result.Succeeded)
			{
				throw new ArgumentException("Invalid password");
			}
			

			return new UserResponseDto
			{
				Name = user.UserName,
				Email = user.Email,
				Role = user.Role,
				Token = _tokenService.CreateToken(user)
			};
		}

		public async Task<UserResponseDto> RegisterAsync(UserDto userDto)
		{
			if (!IsValidRole(userDto.Role))
			{
				throw new ArgumentException("Invalid role. Role must be either 'Admin' or 'Customer'.");
			}

			if (!await _roleManager.RoleExistsAsync(userDto.Role))
			{
				var roleResult = await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
				if (!roleResult.Succeeded)
				{
					throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
				}
			}

			var user = new User
			{
				UserName = userDto.Name,
				Email = userDto.Email,
				Role = userDto.Role,
				Customer=new Customer
				{
					Name=userDto.Name,
					Email=userDto.Email }
				};

			var result = await _userManager.CreateAsync(user, userDto.Password);
			if (!result.Succeeded)
			{
				throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
			}

			var roleAssignmentResult = await _userManager.AddToRoleAsync(user, user.Role);
			if (!roleAssignmentResult.Succeeded)
			{
				throw new Exception(string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description)));
			}

			return new UserResponseDto
			{
				Name = user.UserName,
				Email = user.Email,
				Role = user.Role,
				Token = _tokenService.CreateToken(user)
			};
		}
	}
}
