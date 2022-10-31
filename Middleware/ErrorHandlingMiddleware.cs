using System.Net;
using System.Text.Json;

namespace ApiCatalog.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next; 
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next delegate/middleware in the pipeline
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); // Handle the exception
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}