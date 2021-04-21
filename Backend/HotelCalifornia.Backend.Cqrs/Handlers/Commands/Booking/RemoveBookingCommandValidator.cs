using FluentValidation;
using HotelCalifornia.Backend.Shared.Resources;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class RemoveBookingCommandValidator : AbstractValidator<RemoveBookingCommand>
    {
        public RemoveBookingCommandValidator() 
        {
            
        }
    }
}
