using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaApp.Infrastructure.Middlewares
{

    public class ErrorHandlerMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var responseModel = new { Status = false, Message = "An error occurred while processing request" };

                _logger.LogError(ex, responseModel.Message);
                string result = JsonSerializer.Serialize(
                    responseModel,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
