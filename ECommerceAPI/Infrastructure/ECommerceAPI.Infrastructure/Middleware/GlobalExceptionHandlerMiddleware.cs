using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Infrastructure.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new ErrorResponse();

            switch (exception)
            {
                case BaseException baseEx:
                    errorResponse.Message = baseEx.Message;
                    errorResponse.ErrorCode = baseEx.ErrorCode;
                    errorResponse.StatusCode = baseEx.StatusCode;
                    context.Response.StatusCode = baseEx.StatusCode;

                    if (baseEx is ValidationException validationEx)
                    {
                        errorResponse.ValidationErrors = validationEx.Errors;
                    }

                    _logger.LogWarning(baseEx, "Business exception occured: {Message}", baseEx.Message);
                    break;

                case FluentValidation.ValidationException fluentEx:
                    errorResponse.Message = "Validation failed";
                    errorResponse.ErrorCode = "VALIDATION_ERROR";
                    errorResponse.StatusCode = 400;

                    errorResponse.ValidationErrors = fluentEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    context.Response.StatusCode = 400;
                    _logger.LogWarning(fluentEx, "Validation exception occurred");
                    break;

                default:
                    errorResponse.Message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
                    errorResponse.StatusCode = 500;
                    errorResponse.ErrorCode = "INTERNAL_SERVER_ERROR";
                    context.Response.StatusCode = 500;
                    _logger.LogError(exception, "Unhandled exception occured");
                    break;
            }
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
