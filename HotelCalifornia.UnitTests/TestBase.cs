﻿using Microsoft.Extensions.DependencyInjection;
using HotelCalifornia.Backend.Database;

namespace HotelCalifornia.UnitTests
{
    public class TestBase
    {
        private readonly DatabaseContextFactory FDatabaseContextFactory;

        protected TestBase() 
        {
            var LServices = new ServiceCollection();

            LServices.AddSingleton<DatabaseContextFactory>();
            LServices.AddScoped(AContext =>
            {
                var LFactory = AContext.GetService<DatabaseContextFactory>();
                return LFactory?.CreateDatabaseContext();
            });

            var LServiceScope = LServices.BuildServiceProvider(true).CreateScope();
            var LServiceProvider = LServiceScope.ServiceProvider;
            FDatabaseContextFactory = LServiceProvider.GetService<DatabaseContextFactory>();
        }

        protected DatabaseContext GetTestDatabaseContext()
            =>  FDatabaseContextFactory.CreateDatabaseContext();
    }
}
