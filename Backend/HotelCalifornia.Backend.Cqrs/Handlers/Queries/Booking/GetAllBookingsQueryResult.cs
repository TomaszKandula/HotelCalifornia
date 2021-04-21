using System;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetAllBookingsQueryResult
    {
        public Guid Id { get; set; }
        
        public int RoomNumber { get; set; }
        
        public int Bedrooms { get; set; }
        
        public string GuestFullName { get; set; }
        
        public string GuestPhoneNumber { get; set; }
        
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
