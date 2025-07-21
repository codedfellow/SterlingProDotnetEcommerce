using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Product;
using EcommerceTask.Application.Interfaces.Data;
using EcommerceTask.Application.Interfaces.Services;
using EcommerceTask.Domain.Entities;
using EcommerceTask.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;

        public ProductService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DefaultResponseModel> AddProductAsync(CreateProductDto productDto)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price
            };

            await _context.Products.AddAsync(product);
            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "Product added successfully",
                Success = result > 0,
            };
        }

        public async Task<DefaultResponseModel> DeleteProductAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null)
            {
                throw new CustomException("Product not found");
            }

            _context.Products.Remove(product);
            int result = await _context.SaveChangesAsync(); 

            return new DefaultResponseModel
            {
                Message = "Product deleted successfully",
                Success = result > 0
            };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            var convertedProducts = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price
            });

            return convertedProducts;
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var convertedProduct = new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price
            };

            return convertedProduct;
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var allProducts = await _context.Products.ToListAsync();
            var products = allProducts.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var convertedProducts = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price
            });

            return convertedProducts;
        }

        public async Task<DefaultResponseModel> UpdateProductAsync(ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);

            if (product is null)
            {
                throw new CustomException("Product not found");
            }

            product.ProductName = productDto.ProductName;
            product.Price = productDto.Price;

            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "Product updated successfully",
                Success = result > 0
            };
        }
    }
}
