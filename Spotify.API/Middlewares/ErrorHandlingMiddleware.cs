using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;

namespace Spotify.API.Middlewares
{
    /// <summary>
    /// Global error handler
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor accepting a request delegate and a logger instance (DI)
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke the request and pass it to the next delegate (if there is no exception).
        /// Log the exception (if any) and return a formatted response.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var problem = new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = exception.Message,
                Status = (int)code
            };

            var result = JsonConvert.SerializeObject(
                problem,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
