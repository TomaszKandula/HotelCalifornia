using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using HotelCalifornia.Configuration;
using HotelCalifornia.Backend.Shared.Settings;
using HotelCalifornia.Backend.Shared.Environment;

namespace HotelCalifornia
{
    public class Startup
    {
        private readonly IConfiguration FConfiguration;
        private readonly IWebHostEnvironment FEnvironment;

        public Startup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            FConfiguration = AConfiguration;
            FEnvironment = AEnvironment;
        }
        
        public void ConfigureServices(IServiceCollection AServices)
        {
            AServices.AddMvc(AOption => AOption.CacheProfiles
                .Add("Standard", new CacheProfile
                { 
                    Duration = 10, 
                    Location = ResponseCacheLocation.Any, 
                    NoStore = false 
                }));

            AServices.AddMvc();
            AServices.AddControllers();          
            AServices.AddSpaStaticFiles(AOptions => AOptions.RootPath = "ClientApp/build");
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());
            
            // For E2E testing only when application is bootstrapped in memory
            if (EnvironmentVariables.IsStaging())
                Dependencies.RegisterForTests(AServices, FConfiguration);
            
            // Local development
            if (FEnvironment.IsDevelopment())
            {
                Dependencies.RegisterForTests(AServices, FConfiguration);

                AServices.AddSwaggerGen(AOption =>
                    AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelCaliforniaApi", Version = "v1" }));
            }

            // Production and Staging (deployment slots only)
            if (!FEnvironment.IsProduction() && !FEnvironment.IsStaging()) 
                return;

            Dependencies.Register(AServices, FConfiguration);
        }

        public void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            if (FEnvironment.IsDevelopment())
            {
                AApplication.UseSwagger();
                AApplication.UseSwaggerUI(AOption =>
                    AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1"));
            }

            AApplication.UseEndpoints(AEndpoints => 
                AEndpoints.MapControllers());

            AApplication.UseSpa(ASpa =>
            {
                ASpa.Options.SourcePath = "ClientApp";
                if (FEnvironment.IsDevelopment())
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
            });
        }
    }
}