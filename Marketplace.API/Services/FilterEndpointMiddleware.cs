
namespace Marketplace.API.Services
{
    public class FilterEndpointMiddleware
    {
        private readonly RequestDelegate _next;

        public FilterEndpointMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<FilterEndpointMiddleware> logger)
        {
            string path = context.Request.Path;
            if (path == "/api/PurchasedAdvertisements")
            {
                logger.LogWarning("Trying send request to PurchasedAdvertisements controller.");

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Content is not available");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
