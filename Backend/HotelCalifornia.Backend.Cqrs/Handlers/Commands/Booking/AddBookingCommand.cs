using System;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommand : IRequest<Guid>
    {
        public string GuestFullName { get; set; }

        public string GuestPhoneNumber { get; set; }
        
        public int BedroomsNumber { get; set; }

        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; } 
    }
}
