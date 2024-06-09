
using Assignment_2.CustomException;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assignment_2.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UsernameAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, ex.Message);
            }
            catch (EmailAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Server error");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string message)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            object problem = new
            {
                Status = (int)statusCode,
                Message = statusCode == HttpStatusCode.InternalServerError ? "Server error" : message
            };

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }

    }
}
