using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotelCalifornia.Backend.Database;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetAllArticlesQueryHandler : TemplateHandler<GetAllBookingsQuery, IEnumerable<GetAllBookingsQueryResult>>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        public GetAllArticlesQueryHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<IEnumerable<GetAllBookingsQueryResult>> Handle(GetAllBookingsQuery ARequest, CancellationToken ACancellationToken)
        {
            var LBookings = await FDatabaseContext.Bookings
                .AsNoTracking()
                .Include(ABookings => ABookings.Room)
                .Select(ABookings => new GetAllBookingsQueryResult
                {
                    Id = ABookings.Id,
                    RoomNumber = ABookings.Room.Bedrooms,
                    Bedrooms = ABookings.Room.RoomNumber,
                    GuestFullName = ABookings.GuestFullName,
                    GuestPhoneNumber = ABookings.GuestPhoneNumber,
                    DateFrom = ABookings.DateFrom,
                    DateTo = ABookings.DateTo
                })
                .OrderByDescending(ABookings => ABookings.GuestFullName)
                .ToListAsync(ACancellationToken);
            
            return LBookings;
        }
    }
}
