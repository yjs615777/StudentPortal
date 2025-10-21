using System.Net;
using System.Text.Json;

namespace StudentPortal.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private static readonly JsonSerializerOptions _json = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await next(ctx);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ctx.Response.ContentType = "application/json";

                var problem = new
                {
                    code = "InternalServerError",
                    message = "An unexpected error occurred.",
                    traceId = ctx.TraceIdentifier
                };

                await ctx.Response.WriteAsync(JsonSerializer.Serialize(problem, _json));
            }
        }
    }
}
