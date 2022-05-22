using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Stone.AppStore.Business.Service
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;

        public static readonly string AppName = Namespace[(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1)..];

        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);

            Log.Logger.Information("--------------------- Start application");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IConfiguration GetConfiguration()
        {
            var appSettings = "appsettings.json";

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (!string.IsNullOrWhiteSpace(environment))
            {
                appSettings = string.Format("appsettings.{0}.json", environment);
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appSettings, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static ILogger CreateSerilogLogger(IConfiguration configuration) => new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory +
                    $"/logs/{nameof(Stone.AppStore.Business)}_.log",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: long.Parse(configuration["LogMaxSize"]),
                    retainedFileCountLimit: int.Parse(configuration["RetainedFileCountLimit"]),
                    outputTemplate: "{Timestamp:o} [{Level:u3}] # " +
                                    "{HttpRequestId} # {MachineName}{FullName} " +
                                    "{Message}{NewLine}{Exception}"
                    )
                .CreateLogger();
    }
}
