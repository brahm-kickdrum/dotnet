using Assignment_3.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assignment_3.Middlewares
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
            catch (FailedToAddCustomerException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (CustomerAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (FailedToAddMovieException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (MovieAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (MovieNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, ex.Message);
            }
            catch (RentalAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, ex.Message);
            }
            catch (FailedToAddRentalException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, ex.Message);
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
