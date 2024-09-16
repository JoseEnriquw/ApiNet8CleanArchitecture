using Core.Domain.Classes;
using FluentValidation;
using System.Text.Json;

namespace PruebaApi.Middleware
{
    public sealed class FluentValidationMiddleware : IMiddleware
    {
        private readonly ILogger<FluentValidationMiddleware> _logger;
        public FluentValidationMiddleware(ILogger<FluentValidationMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                if (e is ValidationException)
                    await HandleExceptionAsync(context, e);
                else
                    await HandleExceptionServerAsync(context, e);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            var response = GetErrors(exception);

            response.ForEach(x => _logger.LogError("BadRequest - {x}", x));

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static List<Notify> GetErrors(Exception exception)
        {

            if (exception.InnerException is ValidationException || exception is ValidationException)
            {
                var validationException = (ValidationException)exception;
                return validationException.Errors.Select(x => new Notify
                {
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                    Property = x.PropertyName
                }).ToList();
            }
            else
                return new List<Notify>
                {
                    new Notify
                    {
                        Code = "500 internal server",
                        Property = exception.GetType().ToString(),
                        Message = exception.Message
                    }
                };
        }

        private async Task HandleExceptionServerAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var response = GetErrors(exception);

            response.ForEach(x => _logger.LogError("InternalServerError - {x}", x));

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
