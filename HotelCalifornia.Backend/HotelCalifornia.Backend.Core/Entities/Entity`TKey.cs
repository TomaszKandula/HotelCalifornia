using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCalifornia.Backend.Core.Entities
{
    public abstract class Entity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
