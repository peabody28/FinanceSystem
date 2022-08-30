using logger.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace logger.Operations
{
    public class LoggingOperation : ILoggingOperation
    {
        private ILogger Logger { get; set; }

        public IConfiguration Configuration { get; set; }

        public void Log(string msg)
        {
            if(Configuration.GetSection("Logging").GetSection("Status").Equals(Constants.Enabled))
                Logger.LogInformation(msg);
        }

        public LoggingOperation(IConfiguration configuration)
        {
            Configuration = configuration;
            var logging = Configuration.GetSection("Logging").GetSection("PathFormat").Value;

            Logger = LoggerFactory.Create(builder => builder.AddFile("C:/Temp/Logs/app-{Date}.txt")).CreateLogger<LoggingOperation>();
        }
    }
}
