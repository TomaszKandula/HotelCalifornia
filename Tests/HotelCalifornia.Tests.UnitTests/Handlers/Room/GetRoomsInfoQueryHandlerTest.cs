using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Tests.TestData;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking;

namespace HotelCalifornia.Tests.UnitTests.Handlers.Room
{
    public class GetRoomsInfoQueryHandlerTest : TestBase
    {
        private readonly DateTimeService FDateTimeService;

        public GetRoomsInfoQueryHandlerTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public async Task WhenGetRoomsInfo_ShouldReturnsCollection()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetRoomsInfoQueryHandler = new GetRoomsInfoQueryHandler(LDatabaseContext);
           
            var LRoom = new Rooms
            {
                RoomNumber = 101,
                Bedrooms = 3
            };

            await LDatabaseContext.Rooms.AddAsync(LRoom);
            await LDatabaseContext.SaveChangesAsync();
            
            var LBookings = new List<Bookings>
            {
                new Bookings
                {
                    RoomId = LRoom.Id,
                    GuestFullName = DataProvider.GetRandomString(),
                    GuestPhoneNumber = DataProvider.GetRandomString(9),
                    DateFrom = FDateTimeService.Now.AddDays(5),
                    DateTo = FDateTimeService.Now.AddDays(15)
                },
                new Bookings
                {
                    RoomId = LRoom.Id,
                    GuestFullName = DataProvider.GetRandomString(),
                    GuestPhoneNumber = DataProvider.GetRandomString(9),
                    DateFrom = FDateTimeService.Now.AddDays(1),
                    DateTo = FDateTimeService.Now.AddDays(3)
                }
            };

            await LDatabaseContext.Bookings.AddRangeAsync(LBookings);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LResults = (await LGetRoomsInfoQueryHandler
                .Handle(new GetRoomsInfoQuery(), CancellationToken.None))
                .ToList();
            
            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(1);
            LResults[0].Info.Should().Be("1 room with 3 bedrooms.");
        }
    }
}