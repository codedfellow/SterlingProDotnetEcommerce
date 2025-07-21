using EcommerceTask.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace EcommerceTask.Api.Extensions
{
    internal static class ServiceRegistrationExtensions
    {
        internal static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token in this field",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };

                o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };

                o.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }

        internal static IServiceCollection AddExceptionMiddlewareService(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
            services.AddTransient<TokenMiddleware>();

            return services;
        }
    }
}
