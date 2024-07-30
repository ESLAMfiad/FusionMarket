
//using Order_Management.Core.Entities;

//namespace Orders_Management.Core.Specifications
//{
//	public class ProductWithFilteration :BaseSpecification<Product>
//	{
//		public ProductWithFilteration(ProductSpecParams productParams)
//		: base(x =>
//			(string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search.ToLower())) &&
//			(!productParams.ProductId.HasValue || x.ProductId == productParams.ProductId)
//		)
//		{
//		}
//	}
//}
