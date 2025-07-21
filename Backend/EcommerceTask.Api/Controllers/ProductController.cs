using EcommerceTask.Application.DTOs.Product;
using EcommerceTask.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto productDto)
        {
            var response = await _productService.AddProductAsync(productDto);
            return Ok(response);
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var response = await _productService.DeleteProductAsync(productId);
            return Ok(response);
        }

        [HttpGet("get-product/{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Ok(product);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var response = await _productService.UpdateProductAsync(productDto);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsAsync(string searchTerm)
        {
            var product = await _productService.SearchProductsAsync(searchTerm);
            return Ok(product);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productService.GetAllProductsAsync();
            return Ok(product);
        }
    }
}
