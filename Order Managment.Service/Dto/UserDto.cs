using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Service.Dto
{
	public class UserDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
		[Required]
		public string Role {  get; set; }
	}
}
