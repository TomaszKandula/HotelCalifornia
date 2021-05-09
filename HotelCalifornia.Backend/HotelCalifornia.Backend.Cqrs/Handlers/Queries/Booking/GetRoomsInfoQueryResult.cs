using System;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetRoomsInfoQueryResult
    {
        public Guid Id { get; set; }
        public string Info { get; set; }
    }
}