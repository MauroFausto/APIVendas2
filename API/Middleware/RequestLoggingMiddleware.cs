using System.Diagnostics;
using System.Text;

namespace Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await LogRequest(context);
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            var builder = new StringBuilder();

            var line1 = $"{context.Request.Method} {context.Request.Path}";
            builder.AppendLine(line1);

            foreach (var (key, value) in context.Request.Headers)
            {
                var header = $"{key}: {value}";
                builder.AppendLine(header);
            }

            builder.AppendLine();

            if (context.Request.ContentLength > 0)
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                builder.AppendLine(body);

                context.Request.Body.Position = 0;
            }

            _logger.LogInformation(builder.ToString());
        }
    }
} 