using System.Linq;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Database.Seeders;

namespace HotelCalifornia.Backend.Database.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext FDatabaseContext;

        public DbInitializer(DatabaseContext ADatabaseContext) => FDatabaseContext = ADatabaseContext;

        public void StartMigration() => FDatabaseContext.Database.Migrate();

        public void SeedData()
        {
            if (!FDatabaseContext.Rooms.Any())
                FDatabaseContext.Rooms.AddRange(RoomsSeeder.SeedRooms());

            FDatabaseContext?.SaveChanges();
        }
    }
}
