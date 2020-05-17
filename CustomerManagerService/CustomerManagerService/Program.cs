using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagerService.LoggingConfig;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CustomerManagerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigurationHelper.GetConfiguration();
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(config);
            var telemetryConfiguration = TelemetryConfiguration
                .CreateDefault();
            telemetryConfiguration.InstrumentationKey =
                "f28ac9f1-b56b-44e8-80bb-ffd239ef7a8c";
            loggerConfiguration.WriteTo
                .ApplicationInsights(telemetryConfiguration,
                    TelemetryConverter.Traces);

            Log.Logger = loggerConfiguration
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
