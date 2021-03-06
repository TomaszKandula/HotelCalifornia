using Xunit;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Backend.Core.Generators;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;

namespace HotelCalifornia.UnitTests.Handlers.Booking
{
    public class RemoveBookingCommandHandlerTest : TestBase
    {
        private readonly DateTimeService FDateTimeService;

        public RemoveBookingCommandHandlerTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public async Task GivenCorrectId_WhenRemoveBooking_ShouldSucceed()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveBookingCommandHandler = new RemoveBookingCommandHandler(LDatabaseContext);
           
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

            var LBookingOne = LBookings[0].Id;
            var LBookingTwo = LBookings[1].Id;
            
            // Act
            await LRemoveBookingCommandHandler
                .Handle(new RemoveBookingCommand {Id = LBookingTwo}, CancellationToken.None);      

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LArticlesEntity = await LAssertDbContext.Bookings.FindAsync(LBookingTwo);
            LArticlesEntity.Should().BeNull();            
        }
    }
}