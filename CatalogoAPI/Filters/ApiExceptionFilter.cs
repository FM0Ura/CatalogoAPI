using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {

        _logger.LogError(context.Exception, "Ocorreu um exceção não tratada: Status Code 500");

        context.Result = new ObjectResult("Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}
