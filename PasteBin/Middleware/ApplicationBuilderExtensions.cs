using Microsoft.AspNetCore.Builder;

namespace PasteBinApi.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionsHandling>();
    }
}
