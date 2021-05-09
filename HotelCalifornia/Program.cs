using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotelCalifornia.Backend.Database.Initializer;
using Serilog.Events;
using Serilog;

namespace HotelCalifornia
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        private static readonly bool FIsDevelopment 
            = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;

        public static int Main(string[] AParams)
        {
            try
            {
                Log.Information("Starting WebHost...");
                CreateWebHostBuilder(AParams)
                    .Build()
                    .MigrateDatabase()
                    .Run();
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

        private static IWebHost MigrateDatabase(this IWebHost AWebHost)
        {
            var LServiceScopeFactory = (IServiceScopeFactory) AWebHost.Services.GetService(typeof(IServiceScopeFactory));
            using var LScope = LServiceScopeFactory.CreateScope();
            var LServices = LScope.ServiceProvider;
            var LDbInitializer = LServices.GetRequiredService<IDbInitializer>();

            if (!FIsDevelopment)
                return AWebHost;

            LDbInitializer.StartMigration();
            LDbInitializer.SeedData();

            return AWebHost;
        }
    }
}