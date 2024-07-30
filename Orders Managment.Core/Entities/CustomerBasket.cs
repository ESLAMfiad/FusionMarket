
namespace Order_Management.Core.Entities
{
	public class CustomerBasket
	{
        public CustomerBasket()
        {
			Items = new List<OrderItem>();
		}

        public CustomerBasket(string id): this()
        {
			CustomerBasketId = id;
        }
        public string CustomerBasketId { get; set; }
		
		public List<OrderItem> Items { get; set; }
	}
}
