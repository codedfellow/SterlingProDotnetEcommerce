using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<DefaultResponseModel> AddToCartAsync(AddToCartDto addToCartDto, Guid userId);
        Task<DefaultResponseModel> RemoveFromCartAsync(Guid productId);
        Task<DefaultResponseModel> ClearCartAsync(Guid userId);
        Task<IEnumerable<CartDto>> GetCartItemsAsync(Guid userId);
    }
}
