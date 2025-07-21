using EcommerceTask.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcommerceTask.Api.Middlewares
{
    public class TokenMiddleware : IMiddleware
    {
        private readonly UserSessionInfo _sessionInfo;
        public TokenMiddleware(UserSessionInfo sessionInfo)
        {
            _sessionInfo = sessionInfo;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var emailClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            if (userIdClaim != null)
            {
                _sessionInfo.UserId = Guid.Parse(userIdClaim.Value);
            }

            if (emailClaim != null)
            {
                _sessionInfo.Email = emailClaim.Value;
            }

            await next(context);
        }
    }
}
