using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Service.implementation
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateToken(User user)
		{
			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Role, user.Role)
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
			var token = new JwtSecurityToken(
				//issuer: _configuration["JWT:Issuer"],
				//audience: _configuration["JWT:Audience"],
				claims: claims,
				expires: DateTime.Now.AddDays(30),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
