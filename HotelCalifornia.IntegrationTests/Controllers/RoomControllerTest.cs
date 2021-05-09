using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking;

namespace HotelCalifornia.IntegrationTests.Controllers
{
    public class RoomControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public RoomControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task WhenGetRoomInfo_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            const string REQUEST = "/api/v1/room/getroomsinfo/";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetRoomsInfoQueryResult>>(LContent)
                .ToList();

            LDeserialized.Should().HaveCount(3);
        }
    }
}