using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using EcommerceApi.Exceptions;
namespace EcommerceApi.Middleware
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
            ApiResponse<object> response;
            int statusCode;

            switch (exception)
            {
                case NotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = exception.Message,
                        Data = null
                    };
                    break;

                case ValidationException ve:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = ve.Message,
                        Data = ve.Errors
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = "An unexpected error occurred.",
                        Data = null
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}