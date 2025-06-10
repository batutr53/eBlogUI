using System.Net;
using System.Text.Json;
using eBlog.Shared.Results;

namespace eBlog.API.Middlewares
{
 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new ErrorResult("Beklenmeyen bir hata oluştu. (Global Exception Handler)\n" + exception.Message);


            var json = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(json);
        }
    }

}
