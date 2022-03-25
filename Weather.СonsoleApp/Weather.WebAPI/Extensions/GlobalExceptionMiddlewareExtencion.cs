using Weather.WebAPI.Infrastructure;

namespace Weather.WebAPI.Extensions
{
    public static class GlobalExceptionMiddlewareExtencion
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
