using System.Collections.Generic;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Database.Initializer.Data;

namespace HotelCalifornia.Backend.Database.Initializer.Seeders
{
    public static class RoomsSeeder
    {
        public static IEnumerable<Rooms> SeedRooms()
        {
            return new List<Rooms>
            {
                new Rooms
                {
                    Id = Room101.FId,
                    Bedrooms = Room101.BEDROOMS,
                    RoomNumber = Room101.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room102.FId,
                    Bedrooms = Room102.BEDROOMS,
                    RoomNumber = Room102.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room103.FId,
                    Bedrooms = Room103.BEDROOMS,
                    RoomNumber = Room103.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room104.FId,
                    Bedrooms = Room104.BEDROOMS,
                    RoomNumber = Room104.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room105.FId,
                    Bedrooms = Room105.BEDROOMS,
                    RoomNumber = Room105.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room106.FId,
                    Bedrooms = Room106.BEDROOMS,
                    RoomNumber = Room106.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room107.FId,
                    Bedrooms = Room107.BEDROOMS,
                    RoomNumber = Room107.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room108.FId,
                    Bedrooms = Room108.BEDROOMS,
                    RoomNumber = Room108.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room109.FId,
                    Bedrooms = Room109.BEDROOMS,
                    RoomNumber = Room109.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room110.FId,
                    Bedrooms = Room110.BEDROOMS,
                    RoomNumber = Room110.ROOM_NUMBER
                }
            };
        }
    }
}