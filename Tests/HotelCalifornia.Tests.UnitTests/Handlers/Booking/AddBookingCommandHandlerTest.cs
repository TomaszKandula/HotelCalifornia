using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Tests.TestData;
using HotelCalifornia.Backend.Database;
using HotelCalifornia.Backend.Domain.Entities;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;

namespace HotelCalifornia.Tests.UnitTests.Handlers.Booking
{
    public class AddBookingCommandHandlerTest : TestBase
    {
        private readonly DateTimeService FDateTimeService;

        public AddBookingCommandHandlerTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public async Task AddBooking_WhenAlDataProvided_ShouldAddEntity()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(),
                BedroomsNumber = 3,
                DateFrom = FDateTimeService.Now.AddDays(5),
                DateTo = FDateTimeService.Now.AddDays(15)
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await RoomsFactory(10, LDatabaseContext);
            var LAddBookingCommandHandler = new AddBookingCommandHandler(LDatabaseContext);
            
            // Act
            await LAddBookingCommandHandler.Handle(LAddBookingCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LBookingEntity = LAssertDbContext.Bookings.ToList();
            LBookingEntity.Should().HaveCount(1);
            LBookingEntity[0].GuestFullName.Should().Be(LAddBookingCommand.GuestFullName);
            LBookingEntity[0].GuestPhoneNumber.Should().Be(LAddBookingCommand.GuestPhoneNumber);
            LBookingEntity[0].DateFrom.Should().BeSameDateAs(LAddBookingCommand.DateFrom);
            LBookingEntity[0].DateTo.Should().BeSameDateAs(LAddBookingCommand.DateTo);
        }

        private static async Task RoomsFactory(int ARoomsNumber, DatabaseContext ADatabaseContext)
        {
            var LRooms = new List<Rooms>();
            for (var LIndex = 0; LIndex < ARoomsNumber; LIndex++)
            {
                LRooms.Add(new Rooms
                {
                    Id = new Guid(),
                    RoomNumber = LIndex,
                    Bedrooms = DataProvider.GetRandomInt(1, 3)
                });
            }
            
            ADatabaseContext.Rooms.AddRange(LRooms);
            await ADatabaseContext.SaveChangesAsync();
        }
    }
}