using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;

namespace ScientificPublications
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var currentLocalDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            currentLocalDirectory = currentLocalDirectory.Replace("file:\\", "");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    $"{currentLocalDirectory}..\\..\\..\\..\\..\\Logs\\Log_{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt",
                    LogEventLevel.Warning,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{Exception}")
                    .CreateLogger();

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
