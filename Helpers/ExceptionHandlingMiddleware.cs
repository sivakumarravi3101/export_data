
using System.Net;
using ExportStatistics.Data.Request;
using ExportStatistics.Data.Response;
using ExportStatistics.service;

namespace ExportStatistics.Helpers
{
    public record ExceptionResponse(
        HttpStatusCode StatusCode,
        string Description);

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IErrorLogService errorLog)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(
                        ex,
                        "An exception occurred after the response started.");

                    throw;
                }

                await HandleExceptionAsync(context, ex, errorLog);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            IErrorLogService errorLog)
        {
            var routeData = context.GetRouteData();

            var controllerName =
                routeData.Values["controller"]?.ToString() ?? "Unknown";

            var actionName =
                routeData.Values["action"]?.ToString() ?? "Unknown";

            var error = exception switch
            {
                BadHttpRequestException ex =>
                    new ExceptionResponse(
                        (HttpStatusCode)ex.StatusCode,
                        "Invalid request. Please check your query parameters."),

                ApplicationException =>
                    new ExceptionResponse(
                        HttpStatusCode.BadRequest,
                        "The request could not be processed."),

                KeyNotFoundException =>
                    new ExceptionResponse(
                        HttpStatusCode.NotFound,
                        "The requested resource was not found."),

                UnauthorizedAccessException =>
                    new ExceptionResponse(
                        HttpStatusCode.Unauthorized,
                        "Unauthorized."),

                _ =>
                    new ExceptionResponse(
                        HttpStatusCode.InternalServerError,
                        "Internal server error. Please retry later.")
            };

            if ((int)error.StatusCode >= 500)
            {
                _logger.LogError(
                    exception,
                    "Unhandled exception at {Controller}.{Action}",
                    controllerName,
                    actionName);
            }
            else
            {
                _logger.LogWarning(
                    "Request failed with HTTP {StatusCode} at {Controller}.{Action}",
                    (int)error.StatusCode,
                    controllerName,
                    actionName);
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)error.StatusCode;
            context.Response.ContentType = "application/json";

            var response = new BaseResponse(
                ((int)error.StatusCode).ToString(),
                error.Description);
            var errorLogs = new CreateErrorLogRequest
            {
                ControllerName = controllerName,
                ActionName = actionName,
                StatusCode = (int)error.StatusCode,
                ExceptionType = exception.GetType().FullName,
                Message = exception.Message,
                TraceId = context.TraceIdentifier,
                StackTrace = exception.ToString()
            };
            await errorLog.InsertErrorLogAsync(errorLogs);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
