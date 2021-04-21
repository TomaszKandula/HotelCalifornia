using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelCalifornia.Backend.Cqrs;
using HotelCalifornia.Backend.Database;
using HotelCalifornia.Backend.Shared.Settings;
using HotelCalifornia.Backend.Core.Behaviours;
using HotelCalifornia.Backend.Core.Services.AppLogger;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using FluentValidation;
using MediatR;

namespace HotelCalifornia.Configuration
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection AServices, IConfiguration AConfiguration)
        {
            CommonServices(AServices, AConfiguration);
            SetupDatabase(AServices, AConfiguration);
        }

        public static void RegisterForTests(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            CommonServices(AServices, AConfiguration);
            SetupDatabaseForTest(AServices);
        }

        private static void CommonServices(IServiceCollection AServices, IConfiguration AConfiguration)
        {
            SetupAppSettings(AServices, AConfiguration);
            SetupLogger(AServices);
            SetupServices(AServices);
            SetupValidators(AServices);
            SetupMediatR(AServices);
        }

        private static void SetupAppSettings(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddSingleton(AConfiguration.GetSection("AppUrls").Get<AppUrls>());
        }

        private static void SetupLogger(IServiceCollection AServices) 
            => AServices.AddSingleton<IAppLogger, AppLogger>();

        private static void SetupDatabase(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(AConfiguration.GetConnectionString("DbConnect"),
                AAddOptions => AAddOptions.EnableRetryOnFailure());
            });
        }

        private static void SetupDatabaseForTest(IServiceCollection AServices)
        {
            var LDatabaseName = Guid.NewGuid().ToString();
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                AOptions.EnableSensitiveDataLogging();
                AOptions.UseInMemoryDatabase(LDatabaseName);
            });
        }

        private static void SetupServices(IServiceCollection AServices) 
        {
            AServices.AddHttpContextAccessor();
            AServices.AddScoped<IDateTimeService, DateTimeService>();
            //AServices.AddScoped<IDbInitializer, DbInitializer>();
        }

        private static void SetupValidators(IServiceCollection AServices)
            => AServices.AddValidatorsFromAssemblyContaining<TemplateHandler<IRequest, Unit>>(ServiceLifetime.Scoped);

        private static void SetupMediatR(IServiceCollection AServices) 
        {
            AServices.AddMediatR(AOption => AOption.AsScoped(), 
                typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }    }
}
