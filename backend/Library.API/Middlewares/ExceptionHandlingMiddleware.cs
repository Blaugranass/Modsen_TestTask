using Library.Application.Exceptions;

namespace Library.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            logger.LogInformation("Handling request {Method} {Path}",
                                   context.Request.Method, context.Request.Path);

            await next(context);
            
            logger.LogInformation("Finished handling request");
        }
        catch (CustomException ce)
        {
            context.Response.StatusCode = ce.StatusCode;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                error = ce.ErrorCode,
                message = ce.Message
            };

            logger.LogWarning(ce, 
                "Custom exception occurred: {ErrorCode} - {Message}", 
                ce.ErrorCode, ce.Message);

            await context.Response.WriteAsJsonAsync(payload);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            var payload = new
            {
                error = ex.Message,
                message = "An unexpected error occurred"
            };

            logger.LogError(ex, 
                "Unhandled exception occurred: {Message}", 
                ex.Message);
                
            await context.Response.WriteAsJsonAsync(payload);
        }
    }
}

public static class ExceptionHandlingMiddlewareBuilder
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
