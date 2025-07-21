using EcommerceTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTask.Api.Extensions
{
    internal static class DatabaseExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
