namespace Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
} 