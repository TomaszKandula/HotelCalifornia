using System.Collections.Generic;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetRoomsInfoQuery : IRequest<IEnumerable<GetRoomsInfoQueryResult>> { }
}