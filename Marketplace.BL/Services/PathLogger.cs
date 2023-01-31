using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarketplaceBL.Services
{
    public class PathLogger
    {
        private readonly RequestDelegate _next;
        public PathLogger(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<PathLogger> logger)
        {
            string path = context.Request.Path;
            logger.LogInformation($"Path: {path}, Time: {DateTime.Now.ToLongTimeString()}");

            await _next.Invoke(context);
        }
    }
}
