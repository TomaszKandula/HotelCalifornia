using System;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class RemoveBookingCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
