using EcommerceTask.Application.DTOs.Authentication;
using EcommerceTask.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTask.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationDto model)
        {
            var response = await _authenticationService.RegisterUserAsync(model);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var response = await _authenticationService.LoginAsync(model);
            return Ok(response);
        }
    }
}
