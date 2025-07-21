using EcommerceTask.Application.DTOs;
using EcommerceTask.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceTask.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<DefaultResponseModel> RegisterUserAsync(UserRegistrationDto model);
        Task<DefaultResponseModel> LoginAsync(LoginDto model);
    }
}
