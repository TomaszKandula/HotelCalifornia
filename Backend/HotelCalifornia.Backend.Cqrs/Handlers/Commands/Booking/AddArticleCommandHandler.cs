using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommandHandler : TemplateHandler<AddBookingCommand, Guid>
    {
        
        public AddBookingCommandHandler() 
        {
        }

        public override async Task<Guid> Handle(AddBookingCommand ARequest, CancellationToken ACancellationToken)
        {

            return await Task.FromResult(Guid.Empty);
        }
    }
}
