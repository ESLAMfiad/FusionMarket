
namespace Order_Management.Core.Entities
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string Name { get; set; }
		public string Email {  get; set; }

		public string UserId { get; set; }

		public User User { get; set; }

		public ICollection<Order> Orders { get; set; } = new List<Order>();
	}
}
