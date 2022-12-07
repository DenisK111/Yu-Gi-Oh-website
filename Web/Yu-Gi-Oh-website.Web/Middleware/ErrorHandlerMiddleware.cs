using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MongoDB.Driver.Core.WireProtocol.Messages;
using System.Net;

namespace Yu_Gi_Oh_website.Web.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                logger.LogError($"{error.Source}{Environment.NewLine}{error.Message}{Environment.NewLine}{error.StackTrace}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;               
            }
        }        
    }
}
