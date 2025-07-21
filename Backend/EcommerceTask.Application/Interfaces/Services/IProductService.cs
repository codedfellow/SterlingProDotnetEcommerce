using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid productId);
        Task<DefaultResponseModel> AddProductAsync(CreateProductDto productDto);
        Task<DefaultResponseModel> UpdateProductAsync(ProductDto productDto);
        Task<DefaultResponseModel> DeleteProductAsync(Guid productId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    }
}
