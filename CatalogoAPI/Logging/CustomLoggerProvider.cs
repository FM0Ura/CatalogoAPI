using System.Collections.Concurrent;

namespace CatalogoAPI.Logging;

public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration loggerConfig;

    readonly ConcurrentDictionary<string, CustomLogger> logger = new ConcurrentDictionary<string, CustomLogger>();
    
    public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
    {
        loggerConfig = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return logger.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig));
    }

    public void Dispose()
    {
        logger.Clear();
    }
}
