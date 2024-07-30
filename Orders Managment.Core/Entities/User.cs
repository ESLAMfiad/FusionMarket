using Microsoft.AspNetCore.Identity;


namespace Order_Management.Core.Entities
{
	public class User : IdentityUser
	{
		public Customer Customer { get; set; }
		public string Role { get; set; }
	}
}
