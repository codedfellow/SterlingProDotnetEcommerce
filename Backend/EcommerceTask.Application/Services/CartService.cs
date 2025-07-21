using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Cart;
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
    public class CartService : ICartService
    {
        private readonly IApplicationDbContext _context;
        public CartService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DefaultResponseModel> AddToCartAsync(AddToCartDto addToCartDto, Guid userId)
        {
            bool productInCart = await _context.CartItems.AnyAsync(c => c.ProductId == addToCartDto.ProductId);
            if (productInCart)
            {
                throw new CustomException("Product already in cart");
            }

            var cartItem = new Cart
            {
                ProductId = addToCartDto.ProductId,
                UserId = userId
            };

            await _context.CartItems.AddAsync(cartItem);
            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "Product added to cart successfully",
                Success = result > 0
            };
        }

        public async Task<DefaultResponseModel> ClearCartAsync(Guid userId)
        {
            var cartItems = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();

            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
            }

            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "Cart cleared",
                Success = result > 0
            };
        }

        public async Task<IEnumerable<CartDto>> GetCartItemsAsync(Guid userId)
        {
            var userCartItems = await _context.CartItems
                .Where(c => c.UserId == userId).Join(_context.Products.AsQueryable(), cartItem => cartItem.ProductId, product => product.Id, (cartItem, product) => new CartDto
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    UserId = cartItem.UserId,
                    ProductName = product.ProductName,
                    Price = product.Price
                }).ToListAsync();

            return userCartItems;
        }

        public async Task<DefaultResponseModel> RemoveFromCartAsync(Guid productId)
        {
            var cartItem = await _context.CartItems.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (cartItem is null)
            {
                throw new CustomException("Product not found in in cart");
            }

            _context.CartItems.Remove(cartItem);
            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "Cart cleared",
                Success = result > 0
            };
        }
    } 
}
