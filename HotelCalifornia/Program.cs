using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace HotelCalifornia
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        private static IWebHostBuilder CreateWebHostBuilder(string[] AParams) =>
            WebHost.CreateDefaultBuilder(AParams)
                .UseStartup<Startup>()
                .UseSerilog((AContext, AConfig) =>
                {
                    AConfig.ReadFrom.Configuration(AContext.Configuration);
                    AConfig.WriteTo.Console();
                    AConfig.MinimumLevel.Information();
                    AConfig.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    AConfig.Enrich.FromLogContext();
                });

        public static int Main(string[] AParams)
        {
            try
            {
                Log.Information("Starting WebHost...");
                CreateWebHostBuilder(AParams).Build().Run();
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