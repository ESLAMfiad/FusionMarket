using Order_Management.Core.Entities;
using Order_Management.Service.Dto;
using Orders_Management.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(ProductDto productDto);
        Task<Product> GetProductByIdAsync(int productId);
        Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams productParams);
        Task UpdateProductAsync(int productId, ProductDto productDto);
        Task<int> GetProductCountAsync(ProductSpecParams productParams);

    }
}
