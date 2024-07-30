using Order_Management.Core.Entities;


namespace Orders_Management.Core.Specifications
{
	public class ProductWithSpecification :BaseSpecification<Product>
	{
		public ProductWithSpecification(ProductSpecParams productParams)
		: base(x =>
			(string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search.ToLower()))
			&&
			(!productParams.ProductId.HasValue || x.ProductId == productParams.ProductId)
			)
		{
			ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

			if (!string.IsNullOrEmpty(productParams.Sort))
			{
				switch (productParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDescending(p => p.Price);
						break;
					default:
						AddOrderBy(n => n.Name);
						break;
				}
			}
		}

		public ProductWithSpecification(int id) : base(x => x.ProductId == id)
		{
		}
	}
}
