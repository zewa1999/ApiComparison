using ApiComparison.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiComparison.WebApi.Filters;

public class EntityNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    // de adaugat logging in asta

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is EntityNotFoundException)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }
        return base.OnExceptionAsync(context);
    }
}