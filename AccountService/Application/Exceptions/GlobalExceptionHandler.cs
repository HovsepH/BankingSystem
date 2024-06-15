using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace AccountService.Application.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request."
            };

             if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Title = "One or more validation errors occurred.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Status = StatusCodes.Status400BadRequest;

                var validationErrors = new List<string>();
                foreach (var error in fluentException.Errors)
                {
                    validationErrors.Add(error.ErrorMessage);
                }

                problemDetails.Extensions.Add("errors", validationErrors);
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception occurred.");
                problemDetails.Detail = exception.Message;
            }

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

            return true;
        }
    }
}
