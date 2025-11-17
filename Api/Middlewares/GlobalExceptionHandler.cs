using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Here you can log the exception or modify the response as needed
            httpContext.Response.StatusCode = 500; // Internal Server Error
            httpContext.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
            await httpContext.Response.WriteAsync(result, cancellationToken);
            return true;
        }
    }
}
