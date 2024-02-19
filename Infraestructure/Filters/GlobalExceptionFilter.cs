using Aplication.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Infraestructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception.GetType() == typeof( UserException )) 
            {
                var exception = (UserException) filterContext.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                filterContext.Result = new BadRequestObjectResult(json);
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
