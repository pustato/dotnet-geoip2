using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MaxMind.GeoIP2.Exceptions;
using GeoIP.Responses;

namespace GeoIP.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (AddressNotFoundException)
            {
                var rsp = new ApiResponse<object>
                {
                    Success = false,
                    Msg = "Not Found",
                    Payload = new object(),
                };

                await WriteResponseAsync(rsp, context);
            }
            catch (Exception e)
            {
                var rsp = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Msg = "Internal server error",
                    Payload = e.Message,
                };

                await WriteResponseAsync(rsp, context);
            }
        }

        private Task WriteResponseAsync<T>(ApiResponse<T> response, HttpContext context)
        {
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(response.ToString());
        }
    }
}
