﻿using EventHub.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EventHub.Middlewares
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, ErrorMessages.ServerError);
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
                Message = message
            };

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}
