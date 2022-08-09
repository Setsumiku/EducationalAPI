using Microsoft.EntityFrameworkCore;

namespace EducationalAPI.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public ErrorHandlerMiddleware() { }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NullReferenceException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync("Not Found");
            }
            catch (ArgumentNullException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync("Not Found");
            }
            catch (DbUpdateConcurrencyException e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync("Try again in a moment, database is busy");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync("Threw unexpected exception");
            }
        }
    }
}
