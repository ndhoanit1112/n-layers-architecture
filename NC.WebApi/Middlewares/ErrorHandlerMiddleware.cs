using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NC.Common;
using NC.Common.CustomExceptions;
using NC.Common.Enums;
using NC.WebApi.DTOs.Results;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NC.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
        }

        private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                BusinessException _ => (int)HttpStatusCode.BadRequest,
                NotFoundException _ => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new ApiResponse((int)ResponseCode.Failed, exception.Message));

            context.Response.ContentType = Constants.JsonContentType;
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
