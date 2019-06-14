using System;
using System.Configuration;
using System.Globalization;
using log4net;

namespace NzProducts.Configuration.Management
{
    public static class ConfigurationManager
    {
        private static readonly ILog Logger = LogManager.GetLogger("ConfigurationManager");
        private static readonly AppSettingsSection AppSettings;
        private static readonly NumberFormatInfo Nfi;
        public const string ExtensionsLocation = "C:\\Program Files\\NzProducts";
        private const string FileName = "\\App.config";

        static ConfigurationManager()
        {
            ExeConfigurationFileMap fileMap =
                new ExeConfigurationFileMap { ExeConfigFilename = GetConfigFile() };

            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            AppSettings = config.AppSettings;
            Nfi = new NumberFormatInfo
            {
                NumberGroupSeparator = "",
                CurrencyDecimalSeparator = "."
            };
        }

        public static T GetKey<T>(string name)
        {
            try
            {
                return (T)Convert.ChangeType(AppSettings.Settings[name].Value, typeof(T), Nfi);
            }
            catch (Exception e)
            {
                Logger?.Error($"ConfigurationManager (GetKey) {e.GetType().FullName}: {e.Message} Key name: {name}");
                return default(T);
            }
        }

        public static string GetConfigFile()
        {
            return $"{ExtensionsLocation + FileName}";

        }

    }
}
}
