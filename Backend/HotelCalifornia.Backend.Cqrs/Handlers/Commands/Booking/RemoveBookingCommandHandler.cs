using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class RemoveBookingCommandHandler : TemplateHandler<RemoveBookingCommand, Unit>
    {

        public RemoveBookingCommandHandler() { }

        public override async Task<Unit> Handle(RemoveBookingCommand ARequest, CancellationToken ACancellationToken) 
        {

            return await Task.FromResult(Unit.Value);
        }
    }
}
