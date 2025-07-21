using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Authentication;
using EcommerceTask.Application.Interfaces.Data;
using EcommerceTask.Application.Interfaces.Providers.JwtTokenProvider;
using EcommerceTask.Application.Interfaces.Services;
using EcommerceTask.Domain.Entities;
using EcommerceTask.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenProvider _jwtProvider;

        public AuthenticationService(IApplicationDbContext context, IPasswordHasher<User> passwordHasher, IJwtTokenProvider jwtProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<DefaultResponseModel> LoginAsync(LoginDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user is null)
            {
                throw new CustomException($"Invalid email or password");
            }
            
            var passwordVeificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (passwordVeificationResult == PasswordVerificationResult.Failed)
            {
                throw new CustomException($"Invalid email or password");
            }

            var token = _jwtProvider.Create(user);

            return new DefaultResponseModel
            {
                Message = "Login successful",
                Success = true,
                Data = token
            };
        }

        public async Task<DefaultResponseModel> RegisterUserAsync(UserRegistrationDto model)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Email == model.Email) || await _context.Users.AnyAsync(u => u.UserName == model.UserName);

            if (userExists) {
                throw new CustomException("Credentials already exists");
            };

            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            string passWordHash = _passwordHasher.HashPassword(user, model.Password);
            user.PasswordHash = passWordHash;

            await _context.Users.AddAsync(user);
            int result = await _context.SaveChangesAsync();

            return new DefaultResponseModel
            {
                Message = "User created successfully",
                Success = result > 0
            };
        }
    }
}
