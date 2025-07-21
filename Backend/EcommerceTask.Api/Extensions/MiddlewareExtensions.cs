namespace EcommerceTask.Api.Extensions
{
    internal static class MiddlewareExtensions
    {
        internal static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
