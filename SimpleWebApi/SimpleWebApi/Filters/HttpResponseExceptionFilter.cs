using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleWebApi.Domain.Base.Exceptions;
using System.Net;

namespace SimpleWebApi.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is FieldValidationException httpFielValidationException)
            {
                context.Result = new ObjectResult(httpFielValidationException)
                {
                    StatusCode = httpFielValidationException.StatusCode
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is BusinessRuleException httpBusinessRuleException)
            {
                context.Result = new ObjectResult(httpBusinessRuleException)
                {
                    StatusCode = httpBusinessRuleException.StatusCode
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                context.Result = new ObjectResult(context.Exception)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
