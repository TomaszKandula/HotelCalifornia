using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelCalifornia.Backend.Domain.Entities;

namespace HotelCalifornia.Backend.Database.Mappings
{
    public class RoomsConfiguration : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> AModelBuilder)
        {
            AModelBuilder.Property(ARooms => ARooms.Id).ValueGeneratedOnAdd();
        }
    }
}