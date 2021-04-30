using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Database;
using HotelCalifornia.Backend.Core.Exceptions;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Shared.Resources;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommandHandler : TemplateHandler<AddBookingCommand, AddBookingCommandResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        public AddBookingCommandHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<AddBookingCommandResult> Handle(AddBookingCommand ARequest, CancellationToken ACancellationToken)
        {
            var LRoomsWithBedrooms = await FDatabaseContext.Rooms
                .Where(ARooms => ARooms.Bedrooms == ARequest.BedroomsNumber)
                .Select(ARoom => ARoom.Id)
                .ToListAsync(ACancellationToken);

            if (!LRoomsWithBedrooms.Any())
                throw new BusinessException(
                    nameof(ErrorCodes.REQUESTED_BEDROOMS_UNAVAILABLE),
                    ErrorCodes.REQUESTED_BEDROOMS_UNAVAILABLE);
            
            var LRoomsTaken = await FDatabaseContext.Bookings
                .Where(ABookings => LRoomsWithBedrooms.Contains(ABookings.RoomId) 
                                    && ABookings.DateFrom == ARequest.DateFrom 
                                    && ABookings.DateTo == ARequest.DateTo)
                .Select(ABookings => ABookings.RoomId)
                .ToListAsync(ACancellationToken);

            var LFreeSlots = LRoomsWithBedrooms.Except(LRoomsTaken).ToList();
            
            if (!LFreeSlots.Any())
                throw new BusinessException(nameof(
                    ErrorCodes.NO_AVAILABLE_ROOMS), 
                    ErrorCodes.NO_AVAILABLE_ROOMS);

            var LNewBooking = new Bookings
            {
                RoomId = LFreeSlots.First(),
                GuestFullName = ARequest.GuestFullName,
                GuestPhoneNumber = ARequest.GuestPhoneNumber,
                DateFrom = ARequest.DateFrom,
                DateTo = ARequest.DateTo
            };
            
            FDatabaseContext.Bookings.Add(LNewBooking);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            var LRoomNumber = await FDatabaseContext.Rooms
                .Where(ARooms => ARooms.Id == LNewBooking.RoomId)
                .Select(ARooms => ARooms.RoomNumber)
                .SingleOrDefaultAsync(cancellationToken: ACancellationToken);
            
            return new AddBookingCommandResult
            {
                Id = LNewBooking.Id,
                RoomNumber = LRoomNumber
            };
        }
    }
}
