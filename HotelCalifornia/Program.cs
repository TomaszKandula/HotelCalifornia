using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace HotelCalifornia
{
    public class Program
    {
        private static IWebHostBuilder CreateWebHostBuilder(string[] AArgs) =>
            WebHost.CreateDefaultBuilder(AArgs)
                .UseStartup<Startup>()
                .UseSerilog((AContext, AConfig) =>
                {
                    AConfig.ReadFrom.Configuration(AContext.Configuration);
                    AConfig.WriteTo.Console();
                    AConfig.MinimumLevel.Information();
                    AConfig.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    AConfig.Enrich.FromLogContext();
                });

        public static int Main(string[] AArgs)
        {
            try
            {
                Log.Information("Starting WebHost...");
                CreateWebHostBuilder(AArgs).Build().Run();
                return 0;
            }
            catch (Exception LException)
            {
                Log.Fatal(LException, "WebHost has been terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}