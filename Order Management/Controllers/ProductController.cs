using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Core.Entities;
using Order_Management.Errors;
using Order_Management.Service.Dto;
using Order_Managment.Service.Interfaces;
using Orders_Management.Core.Specifications;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productParams)
		{
			var products = await _productService.GetAllProductsAsync(productParams);
			var productDtos = _mapper.Map<IReadOnlyList<ProductDto>>(products);
			return Ok(productDtos);
		}

		[HttpGet("{productId}")]
		[ProducesResponseType(typeof(ProductDto), 200)] 
		[ProducesResponseType(typeof(ApiResponse),404)]
		public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProductById(int productId)
		{
			var product = await _productService.GetProductByIdAsync(productId);
			if (product == null)
			{
				return NotFound(new ApiResponse(404));
			}
			var productDto = _mapper.Map<ProductDto>(product);
			return Ok(productDto);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse), 201)]
		[ProducesResponseType(typeof(ApiResponse), 400)]
		public async Task<IActionResult> AddProduct(ProductDto productDto)
		{
			var createdProduct = await _productService.AddProductAsync(productDto);
			var productDtoResponse = _mapper.Map<ProductDto>(createdProduct);
			return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.ProductId },
				new ApiResponse(201, "Product created successfully"));

		}



		[HttpPut("{productId}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ApiResponse), 200)]
		[ProducesResponseType(typeof(ApiResponse), 404)]
		public async Task<IActionResult> UpdateProduct(int productId, ProductDto productDto)
		{
			var product = await _productService.GetProductByIdAsync(productId);
			if (product == null)
			{
				return NotFound(new ApiResponse(404));
			}
			await _productService.UpdateProductAsync(productId, productDto);
			return Ok(new ApiResponse(200, "Product updated successfully"));
		}
	}
}
