using FluentValidation;
using HotelCalifornia.Backend.Shared.Resources;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator() 
        {
            
        }
    }
}
