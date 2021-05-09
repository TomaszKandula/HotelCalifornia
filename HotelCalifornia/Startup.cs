using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using HotelCalifornia.Middleware;
using HotelCalifornia.Configuration;
using HotelCalifornia.Backend.Shared.Settings;
using Serilog;

namespace HotelCalifornia
{
    [ExcludeFromCodeCoverage]
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
            AServices.AddMvc();
            AServices.AddControllers();          
            AServices.AddSpaStaticFiles(AOptions => AOptions.RootPath = "ClientApp/build");
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());
            Dependencies.Register(AServices, FConfiguration);
            
            if (FEnvironment.IsDevelopment())
            {
                AServices.AddSwaggerGen(AOption =>
                    AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelCaliforniaApi", Version = "v1" }));
            }
        }

        public void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            AApplication.UseSerilogRequestLogging();
            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<CustomCors>();
            
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