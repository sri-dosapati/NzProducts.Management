using System;
using System.Configuration;

namespace NzProducts.Configuration.Management
{
    public static class ConfigurationManager
    {
        private const string FileName = "\\App.config";

        static ConfigurationManager()
        {
          
            ExeConfigurationFileMap fileMap =
                new ExeConfigurationFileMap { ExeConfigFilename = GetConfigFile() };

            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

        }
      
        public static string GetConfigFile()
        {
            var extensionsLocation = AppDomain.CurrentDomain.BaseDirectory;
            return $"{extensionsLocation + FileName}";

        }

    }
}

