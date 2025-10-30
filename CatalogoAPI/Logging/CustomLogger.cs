namespace CatalogoAPI.Logging;

public class CustomLogger : ILogger
{
    private string loggerName;
    private CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerName = name;
        this.loggerConfig = loggerConfig;
    }

    public bool IsEnabled(LogLevel level) => level == LogLevel.Information;

    public IDisposable? BeginScope<TState>(TState state) => null;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        string message = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
        WriteLog(message);
    }
    private void WriteLog(string message)
    {
        string caminhoArquivoLog = @"C:\Users\felip\source\repos\FM0Ura\CatalogoAPI\CatalogoAPI\Log\Logs.log";

        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
