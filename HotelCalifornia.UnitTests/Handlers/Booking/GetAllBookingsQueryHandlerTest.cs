using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Backend.Core.Generators;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking;

namespace HotelCalifornia.UnitTests.Handlers.Booking
{
    public class GetAllBookingsQueryHandlerTest : TestBase
    {
        private readonly DateTimeService FDateTimeService;

        public GetAllBookingsQueryHandlerTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public async Task WhenGetAllBookings_ShouldReturnsCollection()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllBookingsQueryHandler = new GetAllBookingsQueryHandler(LDatabaseContext);
           
            var LRoom = new Rooms
            {
                RoomNumber = 101,
                Bedrooms = 3
            };

            await LDatabaseContext.Rooms.AddAsync(LRoom);
            await LDatabaseContext.SaveChangesAsync();
            
            var LBookings = new List<Bookings>
            {
                new ()
                {
                    RoomId = LRoom.Id,
                    GuestFullName = StringProvider.GetRandomString(),
                    GuestPhoneNumber = StringProvider.GetRandomString(9),
                    DateFrom = FDateTimeService.Now.AddDays(5),
                    DateTo = FDateTimeService.Now.AddDays(15)
                },
                new ()
                {
                    RoomId = LRoom.Id,
                    GuestFullName = StringProvider.GetRandomString(),
                    GuestPhoneNumber = StringProvider.GetRandomString(9),
                    DateFrom = FDateTimeService.Now.AddDays(1),
                    DateTo = FDateTimeService.Now.AddDays(3)
                }
            };

            await LDatabaseContext.Bookings.AddRangeAsync(LBookings);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LResults = (await LGetAllBookingsQueryHandler
                .Handle(new GetAllBookingsQuery(), CancellationToken.None))
                .ToList();
            
            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
        }
    }
}