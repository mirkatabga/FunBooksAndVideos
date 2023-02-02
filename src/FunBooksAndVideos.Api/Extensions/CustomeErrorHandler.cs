using System.Diagnostics;
using System.Text.Json;
using FunBooksAndVideos.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.API.Extensions
{
    public static class CustomErrorHandler
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex != null)
            {
                httpContext.Response.ContentType = "application/problem+json";
                ProblemDetails? problem;

                if (ex is ValidationException validationException)
                {
                    const string title = "Validation error.";
                    var details = JsonSerializer.Serialize(validationException.Errors);

                    problem = CreateProblem(StatusCodes.Status400BadRequest, title, details);

                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await WriteToResponse(httpContext, problem);
                }
                else if (ex is NotFoundException notFoundException)
                {
                    const string title = "Item not found.";
                    var details = notFoundException.Message;
                    problem = CreateProblem(StatusCodes.Status500InternalServerError, title, details);

                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await WriteToResponse(httpContext, problem);
                }
                else
                {
                    var title = includeDetails ? "An error occurred: " + ex.Message : "An error occurred";
                    var details = includeDetails ? ex.ToString() : null;
                    problem = CreateProblem(StatusCodes.Status500InternalServerError, title, details);

                    await WriteToResponse(httpContext, problem);
                }
            }
        }

        private static ProblemDetails CreateProblem(int status, string title, string? details)
        {
            return new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = details
            };
        }

        private static async Task WriteToResponse(HttpContext httpContext, ProblemDetails? problem)
        {
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problem!.Extensions["traceId"] = traceId;
            }

            var stream = httpContext!.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}