using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Practice5.Practice5API.Filters.ExceptionFilters
{
    public class CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger = logger;

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An error occurred");
            context.Result = new ObjectResult(new { error = "An error occurred" })
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }

}