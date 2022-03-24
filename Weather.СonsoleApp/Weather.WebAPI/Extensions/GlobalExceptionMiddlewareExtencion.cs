using Weather.WebAPI.Middleware;

namespace Weather.WebAPI.Extensions
{
    public static class GlobalExceptionMiddlewareExtencion
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<HandleExceptionMiddleware>();
        }
    }
}
