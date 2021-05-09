using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCalifornia.Backend.Core.Entities
{
    public abstract class Entity<TKey>
    {
        [Key]
        public Guid Id { get; set; }
    }
}
