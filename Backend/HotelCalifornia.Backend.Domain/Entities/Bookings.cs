using System;
using System.ComponentModel.DataAnnotations;
using HotelCalifornia.Backend.Core.Entities;

namespace HotelCalifornia.Backend.Domain.Entities
{
    public class Bookings : Entity<Guid>
    {
        [Required]
        public int RoomId { get; set; }

        public Rooms Room { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string GuestFullName { get; set; }

        [Required]
        [MaxLength(12)]
        public string GuestPhoneNumber { get; set; }
        
        [Required]
        public DateTime DateFrom { get; set; }
        
        [Required]
        public DateTime DateTo { get; set; }
    }
}