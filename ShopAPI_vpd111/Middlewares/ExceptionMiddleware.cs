using BusinessLogic.ApiModels;
using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace ShopAPI_vpd111.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
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
            catch (HttpException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ExceptionMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ExceptionMiddleware>();
    //    }
    //}
}
