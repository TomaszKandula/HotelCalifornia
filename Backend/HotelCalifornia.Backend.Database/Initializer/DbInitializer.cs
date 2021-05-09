using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelCalifornia.Backend.Database.Seeders;

namespace HotelCalifornia.Backend.Database.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory FScopeFactory;

        public DbInitializer(IServiceScopeFactory AScopeFactory)
            => FScopeFactory = AScopeFactory;

        public void StartMigration()
        {
            using var LServiceScope = FScopeFactory.CreateScope();
            using var LDatabaseContext = LServiceScope.ServiceProvider.GetService<DatabaseContext>();

            if (LDatabaseContext?.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory") 
                LDatabaseContext?.Database.Migrate();
        }

        public async Task SeedData()
        {
            using var LServiceScope = FScopeFactory.CreateScope();
            await using var LDatabaseContext = LServiceScope.ServiceProvider.GetService<DatabaseContext>();

            if (LDatabaseContext != null && !LDatabaseContext.Rooms.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.Rooms.AddRange(RoomsSeeder.SeedRooms());

            await LDatabaseContext?.SaveChangesAsync();
        }
    }
}
