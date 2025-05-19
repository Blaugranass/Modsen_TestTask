namespace Library.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsync(ex.Message);
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
