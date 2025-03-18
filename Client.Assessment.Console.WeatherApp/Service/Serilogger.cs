using Serilog;

namespace Client.Assessment.Console.WeatherApp.Service
{
    /// <summary>
    /// Logger class to log the information
    /// </summary>
    public static class Serilogger
    {
        public static readonly ILogger Instance;

        static Serilogger()
        {
            Instance = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/WeatherApp.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
