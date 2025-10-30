using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoAPI.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger) => _logger = logger;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Executa depois da Action
        _logger.LogInformation("EXECUTANDO -> OnActionExecuted");
        _logger.LogInformation("---------------------------------------");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"StatusCode : {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("---------------------------------------");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Executa antes da Action
        _logger.LogInformation("EXECUTANDO -> OnActionExecuting");
        _logger.LogInformation("---------------------------------------");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        _logger.LogInformation("---------------------------------------");
    }
}
