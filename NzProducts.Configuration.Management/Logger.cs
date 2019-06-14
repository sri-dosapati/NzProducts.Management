using System.IO;
using log4net;
using log4net.Config;

namespace NzProducts.Configuration.Management
{
    public static class Logger
    {
        private static readonly string LogConfigFile = ConfigurationManager.GetConfigFile();
        public static ILog GetLogger(string logger)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(LogConfigFile));
            return LogManager.GetLogger(logger);
        }
    }
}