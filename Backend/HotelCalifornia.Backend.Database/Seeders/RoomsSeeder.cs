using System.Collections.Generic;
using HotelCalifornia.Backend.Database.Dummies;
using HotelCalifornia.Backend.Domain.Entities;

namespace HotelCalifornia.Backend.Database.Seeders
{
    public static class RoomsSeeder
    {
        public static IEnumerable<Rooms> SeedRooms()
        {
            return new List<Rooms>
            {
                new Rooms
                {
                    Id = Room1.FId,
                    Bedrooms = Room1.BEDROOMS,
                    RoomNumber = Room1.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room2.FId,
                    Bedrooms = Room2.BEDROOMS,
                    RoomNumber = Room2.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room3.FId,
                    Bedrooms = Room3.BEDROOMS,
                    RoomNumber = Room3.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room4.FId,
                    Bedrooms = Room4.BEDROOMS,
                    RoomNumber = Room4.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room5.FId,
                    Bedrooms = Room5.BEDROOMS,
                    RoomNumber = Room5.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room6.FId,
                    Bedrooms = Room6.BEDROOMS,
                    RoomNumber = Room6.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room7.FId,
                    Bedrooms = Room7.BEDROOMS,
                    RoomNumber = Room7.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room8.FId,
                    Bedrooms = Room8.BEDROOMS,
                    RoomNumber = Room8.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room9.FId,
                    Bedrooms = Room9.BEDROOMS,
                    RoomNumber = Room9.ROOM_NUMBER
                },
                new Rooms
                {
                    Id = Room10.FId,
                    Bedrooms = Room10.BEDROOMS,
                    RoomNumber = Room10.ROOM_NUMBER
                }
            };
        }
    }
}