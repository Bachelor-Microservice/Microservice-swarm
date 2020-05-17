using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.LoggingConfig
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration(string userSecretsKey = null)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            if (!string.IsNullOrWhiteSpace(envName))
                builder.AddJsonFile($"appsettings.{envName}.json", optional: true);
            if (!string.IsNullOrWhiteSpace(userSecretsKey))
                builder.AddUserSecrets(userSecretsKey);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
