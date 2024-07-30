

namespace Orders_Management.Core.Specifications
{
	public class ProductSpecParams 
	{
		private const int MaxPageSize = 12;
		public int PageIndex { get; set; } = 1;
		private int _pageSize = 6;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}

		public string? Sort { get; set; }
		public int? ProductId { get; set; }
		public string? Search { get; set; }
	}
}
