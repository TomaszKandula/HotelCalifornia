using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Database;
using HotelCalifornia.Backend.Core.Exceptions;
using HotelCalifornia.Backend.Shared.Resources;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class RemoveBookingCommandHandler : TemplateHandler<RemoveBookingCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        public RemoveBookingCommandHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<Unit> Handle(RemoveBookingCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LCurrentBooking = await FDatabaseContext.Bookings
                .Where(ABookings => ABookings.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);
            
            if (!LCurrentBooking.Any())
                throw new BusinessException(nameof(ErrorCodes.BOOKING_DOES_NOT_EXISTS), ErrorCodes.BOOKING_DOES_NOT_EXISTS);

            FDatabaseContext.Bookings.Remove(LCurrentBooking.First());
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}
