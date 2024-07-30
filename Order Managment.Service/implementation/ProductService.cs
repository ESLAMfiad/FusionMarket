using AutoMapper;
using Order_Management.Core.Entities;
using Order_Management.Repository.Repo_Implementation;
using Order_Management.Service.Dto;
using Order_Management.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders_Management.Core.Specifications;
using Order_Managment.Service.Interfaces;
using Orders_Managment.Core;

namespace Order_Management.Service.implementation
{
    public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<Product> AddProductAsync(ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);
			await _unitOfWork.Repository<Product>().AddAsync(product);
			return product;
		}

		public async Task<Product> GetProductByIdAsync(int productId)
		{
			return await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
		}

		public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams productParams)
		{
			var spec = new ProductWithSpecification(productParams);
			return await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
		}

		public async Task UpdateProductAsync(int productId, ProductDto productDto)
		{
			var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
			if (product == null)
			{
				throw new ArgumentException($"Product with ID {productId} not found.");
			}

			_mapper.Map(productDto, product);
			await _unitOfWork.Repository<Product>().UpdateAsync(product);
		}

		public async Task<int> GetProductCountAsync(ProductSpecParams productParams)
		{
			var spec = new ProductWithSpecification(productParams);
			return await _unitOfWork.Repository<Product>().CountAsync(spec);
		}
	}
}
