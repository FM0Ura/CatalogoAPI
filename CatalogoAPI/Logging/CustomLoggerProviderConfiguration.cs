namespace CatalogoAPI.Logging;

public class CustomLoggerProviderConfiguration
{
    public LogLevel LogLevel { get; set; } = LogLevel.Debug;
    public int EventId { get; set; } = 0;
}
