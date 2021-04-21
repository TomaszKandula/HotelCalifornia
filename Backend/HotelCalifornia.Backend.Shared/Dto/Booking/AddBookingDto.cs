using System;

namespace HotelCalifornia.Backend.Shared.Dto.Booking
{
    public class AddBookingDto
    {
        public string GuestFullName { get; set; }

        public string GuestPhoneNumber { get; set; }
        
        public int BedroomsNumber { get; set; }

        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; } 
    }
}