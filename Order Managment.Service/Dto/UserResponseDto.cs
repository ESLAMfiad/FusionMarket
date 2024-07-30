using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Dto
{
	public class UserResponseDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string Token { get; set; }
	}
}
