using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelCalifornia.Backend.Core.Entities;

namespace HotelCalifornia.Backend.Domain.Entities
{
    public class Rooms : Entity<Guid>
    {
        public Rooms()
        {
            Bookings = new HashSet<Bookings>();
        }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int Bedrooms { get; set; }
        
        public ICollection<Bookings> Bookings { get; set; }
    }
}