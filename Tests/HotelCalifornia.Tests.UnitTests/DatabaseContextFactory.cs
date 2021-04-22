using System;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Database;

namespace HotelCalifornia.Tests.UnitTests
{
    internal class DatabaseContextFactory
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        public DatabaseContext CreateDatabaseContext()
            =>  new DatabaseContext(FDatabaseOptions.Options);
    }
}
