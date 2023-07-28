using System.Net;
using System.Text.Json;
using TMS.Exceptions;

namespace TMS.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate nextRequestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _nextRequestDelegate = nextRequestDelegate;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _nextRequestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            string message;
            switch (exception)
            {
                case EntityNotFoundException ex:

                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;

                case ArgumentNullException ex:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                default:
                    if (exception.Message.Contains("Invalid Token"))
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        message = exception.Message;
                        break;
                    }
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Internal server error!";
                    break;
            }
            _logger.LogError(exception.Message, exception.StackTrace);
            var result = JsonSerializer.Serialize(new { errorMessage = message });
            await httpContext.Response.WriteAsync(result);
        }
    }
}
