using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoList.Filers;

public class SimpleResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.Headers.Append("Test-Header", "Test Header Value");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}