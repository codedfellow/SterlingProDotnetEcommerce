using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcommerceTask.Api
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            string secret = configuration["Jwt:Secret"]!;

            string[] allowedOrigins = configuration["AllowedOrigins"]?.Split(",") ?? new string[0];

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.WithOrigins(allowedOrigins).AllowAnyHeader()
                                                              .AllowAnyMethod().AllowCredentials();
                                  });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(secret);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false, 
                        ValidateAudience = false, 
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
            services.AddAuthorization();
            services.AddControllers();

            return services;
        }
    }
}
