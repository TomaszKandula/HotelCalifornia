using System;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommandResult
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
    }
}