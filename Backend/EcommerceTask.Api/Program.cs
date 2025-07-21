using EcommerceTask.Api;
using EcommerceTask.Api.Extensions;
using EcommerceTask.Api.Middlewares;
using EcommerceTask.Application;
using EcommerceTask.Infrastructure;

namespace SterlingProDotnetTask
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerWithAuthentication().AddExceptionMiddlewareService().AddApi(builder.Configuration).AddInfrastructureServices(builder.Configuration).AddApplication();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerWithUI();
                app.ApplyMigrations();
            }

            app.UseCors("AllowSpecificOrigins");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenMiddleware>();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
