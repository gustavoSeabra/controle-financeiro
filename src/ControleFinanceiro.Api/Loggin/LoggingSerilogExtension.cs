using Serilog.Core;
using Serilog.Events;
using Serilog;

namespace ControleFinanceiro.Api.Loggin
{
    public static class LoggingSerilogExtension
    {
        private static readonly LogEventLevel _defaultLogLevel = LogEventLevel.Information;
        private static readonly LoggingLevelSwitch _loggingLevel = new LoggingLevelSwitch();

        private static void LoadLogLevel()
        {
            var configLogLevel = Environment.GetEnvironmentVariable("LOG_LEVEL") ?? _defaultLogLevel.ToString();

            bool parsed = Enum.TryParse(configLogLevel, true, out LogEventLevel logLevel);
            _loggingLevel.MinimumLevel = parsed ? logLevel : _defaultLogLevel;
        }

        /// <summary>
        /// ConfigureLog
        /// </summary>
        private static void ConfigureLog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.ControlledBy(_loggingLevel)
                .CreateLogger();
        }


        /// <summary>
        /// Add Logging Serilog
        /// </summary>
        public static IServiceCollection AddLoggingSerilog(this IServiceCollection services)
        {
            LoadLogLevel();
            ConfigureLog();

            return services;
        }
    }
}
