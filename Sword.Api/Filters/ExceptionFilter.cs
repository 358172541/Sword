using Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Api.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            HandleNotFoundException(context);
            HandleDbUpdateConcurrencyException(context);
            HandleTokenException(context);
            HandleValidationException(context);
            return Task.CompletedTask;
        }
        private static void HandleNotFoundException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException exception)
            {
                context.HttpContext.Response.Headers.Add("X-Entity", "Empty");
                context.Result = new ObjectResult(JsonConvert.SerializeObject(new string[] { exception.Message })) { StatusCode = StatusCodes.Status404NotFound };
            }
        }
        private static void HandleDbUpdateConcurrencyException(ExceptionContext context)
        {
            if (context.Exception is DbUpdateConcurrencyException exception)
            {
                context.HttpContext.Response.Headers.Add("X-Version", "Conflict");
                context.Result = new ObjectResult(JsonConvert.SerializeObject(new string[] { exception.Message })) { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
        private static void HandleTokenException(ExceptionContext context)
        {
            if (context.Exception is TokenException exception)
            {
                context.HttpContext.Response.Headers.Add("X-Token", "Expired");
                context.Result = new ObjectResult(JsonConvert.SerializeObject(new string[] { exception.Message })) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
        private static void HandleValidationException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.Headers.Add("X-Validation", "Failed");
                context.Result = new ObjectResult(JsonConvert.SerializeObject(new string[] { exception.Message })) { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
    }
}
