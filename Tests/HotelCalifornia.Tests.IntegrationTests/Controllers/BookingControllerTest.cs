using System;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Tests.TestData;
using HotelCalifornia.Backend.Shared.Resources;
using HotelCalifornia.Backend.Shared.Dto.Booking;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;
using HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking;

namespace HotelCalifornia.Tests.IntegrationTests.Controllers
{
    public class BookingControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;
        
        private readonly DateTimeService FDateTimeService = new();
        
        public BookingControllerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task WhenGetAllBookings_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            const string REQUEST = "/api/v1/booking/getallbookings/";
            var LHttpClient = FWebAppFactory.CreateClient();
            
            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);
            
            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllBookingsQueryResult>>(LContent)
                .ToList();

            LDeserialized.Should().HaveCount(0);
        }

        [Fact]
        public async Task GivenCorrectData_WhenAddBooking_ShouldReturnNewGuidAndRoomNumber()
        {
            // Arrange
            const string REQUEST = "/api/v1/booking/addbooking/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddBookingDto
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = "48111222333",
                BedroomsNumber = DataProvider.GetRandomInt(1, 3),
                DateFrom = FDateTimeService.Now,
                DateTo = FDateTimeService.Now.AddDays(1)
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            
            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<AddBookingCommandResult>(LContent);
            LDeserialized.RoomNumber.Should().BeGreaterThan(0);
            GuidTest.Check(LDeserialized.Id.ToString()).Should().BeTrue();
        }

        [Fact]
        public async Task GivenTooManyBedrooms_WhenAddBooking_ShouldThrowError()
        {
            // Arrange
            const string REQUEST = "/api/v1/booking/addbooking/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddBookingDto
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = "48111222333",
                BedroomsNumber = DataProvider.GetRandomInt(10, 100),
                DateFrom = FDateTimeService.Now,
                DateTo = FDateTimeService.Now.AddDays(1)
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            
            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.REQUESTED_BEDROOMS_UNAVAILABLE);
        }
        
        [Fact]
        public async Task GivenNonExistingBookingId_WhenRemoveBooking_ShouldThrowError()
        {
            // Arrange
            const string REQUEST = "/api/v1/booking/removebooking/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new RemoveBookingDto { BookingId = Guid.NewGuid() };
            var LHttpClient = FWebAppFactory.CreateClient();

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            
            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.BOOKING_DOES_NOT_EXISTS);
        }
    }
}