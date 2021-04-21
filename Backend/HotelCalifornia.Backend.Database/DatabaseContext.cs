using System.Reflection;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Domain.Entities;

namespace HotelCalifornia.Backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> AOptions) : base(AOptions) { }

        public virtual DbSet<Rooms> Rooms { get; set; }
        
        public virtual DbSet<Bookings> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {
            base.OnModelCreating(AModelBuilder);
            ApplyConfiguration(AModelBuilder);
        }

        private void ApplyConfiguration(ModelBuilder AModelBuilder) 
            => AModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
