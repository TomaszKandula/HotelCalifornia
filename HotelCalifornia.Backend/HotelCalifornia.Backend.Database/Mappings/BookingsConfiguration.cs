using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelCalifornia.Backend.Domain.Entities;

namespace HotelCalifornia.Backend.Database.Mappings
{
    public class BookingsConfiguration: IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> AModelBuilder)
        {
            AModelBuilder.Property(ABookings => ABookings.Id).ValueGeneratedOnAdd();

            AModelBuilder
                .HasOne(ABookings => ABookings.Room)
                .WithMany(ARooms => ARooms.Bookings)
                .HasForeignKey(ABookings => ABookings.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Rooms");
        }
    }
}