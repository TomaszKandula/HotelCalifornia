using Xunit;
using FluentAssertions;
using HotelCalifornia.Tests.TestData;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;
using HotelCalifornia.Backend.Shared.Resources;

namespace HotelCalifornia.Tests.UnitTests.Validators
{
    public class AddBookingCommandValidatorTest
    {
        private readonly DateTimeService FDateTimeService;

        public AddBookingCommandValidatorTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public void AddBooking_WhenAllFieldsAreCorrect_ShouldSucceed()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(9),
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now.AddDays(10),
                DateTo = FDateTimeService.Now.AddDays(20)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }
        
        [Fact]
        public void AddBooking_WhenNoGuestFullNameAndPhoneNumberProvided_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = null,
                GuestPhoneNumber = null,
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now.AddDays(10),
                DateTo = FDateTimeService.Now.AddDays(20)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void AddBooking_WhenGuestFullNameTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(300),
                GuestPhoneNumber = DataProvider.GetRandomString(9),
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now.AddDays(10),
                DateTo = FDateTimeService.Now.AddDays(20)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.GUEST_NAME_TOO_LONG));
        }
        
        [Fact]
        public void AddBooking_WhenGuestPhoneNumberTooLong_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(15),
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now.AddDays(10),
                DateTo = FDateTimeService.Now.AddDays(20)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.PHONE_NUMBER_TOO_LONG));
        }

        [Fact]
        public void AddBooking_WhenBedroomsNumberIsWrong_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(9),
                BedroomsNumber = -10,
                DateFrom = FDateTimeService.Now.AddDays(10),
                DateTo = FDateTimeService.Now.AddDays(20)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
        
        [Fact]
        public void AddBooking_WhenDateFromAndDateToAreSame_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(9),
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now,
                DateTo = FDateTimeService.Now
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.START_DATE_AND_END_DATE_CANNOT_BE_SAME));
        }
        
        [Fact]
        public void AddBooking_WhenDateToIsEarlierThanDateFrom_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = DataProvider.GetRandomString(),
                GuestPhoneNumber = DataProvider.GetRandomString(9),
                BedroomsNumber = 1,
                DateFrom = FDateTimeService.Now.AddDays(1),
                DateTo = FDateTimeService.Now.AddDays(-10)
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_TODAY));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_STAR_DATE));
        }
        
    }
}