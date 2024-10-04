using Microsoft.AspNetCore.Mvc.Filters;

namespace Practice5.Practice5API.Filters.ActionFilters
{
    public class LogActionFilter(ILogger<LogActionFilter> logger) : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger = logger;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action Executing: {ActionName}", context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action Executed: {ActionName}", context.ActionDescriptor.DisplayName);
        }
    }
}