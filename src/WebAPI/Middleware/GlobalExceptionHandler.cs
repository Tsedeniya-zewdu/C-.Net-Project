using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Middleware
{
    // Catches every exception thrown while handling a request and turns it into
    // a standard "problem details" JSON response with the right status code.
    // This is why controllers and handlers never need try/catch.
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails problem = exception switch
            {
                ValidationException validation => new ValidationProblemDetails(
                    validation.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()))
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred."
                },
                BadRequestException badRequest => new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = badRequest.Message,
                    Extensions = { ["errors"] = badRequest.Errors }
                },
                UnauthorizedException => Problem(StatusCodes.Status401Unauthorized, "Unauthorized", exception.Message),
                ForbiddenException => Problem(StatusCodes.Status403Forbidden, "Forbidden", exception.Message),
                NotFoundException => Problem(StatusCodes.Status404NotFound, "Not found", exception.Message),
                ConflictException => Problem(StatusCodes.Status409Conflict, "Conflict", exception.Message),

                // Anything else is a bug. Log it, but never send internal details to the client.
                _ => Problem(StatusCodes.Status500InternalServerError, "Server error", "An unexpected error occurred.")
            };

            if (problem.Status == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception, "Unhandled exception for {Method} {Path}",
                    httpContext.Request.Method, httpContext.Request.Path);
            }

            httpContext.Response.StatusCode = problem.Status!.Value;
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem,
                Exception = exception
            });
        }

        private static ProblemDetails Problem(int status, string title, string detail)
            => new() { Status = status, Title = title, Detail = detail };
    }
}
