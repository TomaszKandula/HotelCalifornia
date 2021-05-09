using Xunit;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using HotelCalifornia.Backend.Core.Generators;
using HotelCalifornia.Backend.Shared.Resources;
using HotelCalifornia.Backend.Core.Services.DateTimeService;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;

namespace HotelCalifornia.UnitTests.Validators
{
    public class AddBookingCommandValidatorTest
    {
        private readonly DateTimeService FDateTimeService;

        public enum TestCaseDays 
        {
            Present,
            Past,
            Future
        }

        public AddBookingCommandValidatorTest() => FDateTimeService = new DateTimeService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenAddBooking_ShouldSucceed()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(),
                GuestPhoneNumber = StringProvider.GetRandomString(9),
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
        public void GivenNoGuestFullNameAndPhoneNumberProvided_WhenAddBooking_ShouldThrowError()
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
        public void GivenGuestFullNameTooLong_WhenAddBooking_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(300),
                GuestPhoneNumber = StringProvider.GetRandomString(9),
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
        public void GivenGuestPhoneNumberTooLong_WhenAddBooking_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(),
                GuestPhoneNumber = StringProvider.GetRandomString(15),
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
        public void GivenBedroomsNumberIsWrong_WhenAddBooking_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(),
                GuestPhoneNumber = StringProvider.GetRandomString(9),
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
        
        [Theory]
        [ClassData(typeof(DateTimeTestCases))]
        public void GivenDateFromAndDateToAreSame_WhenAddBooking_ShouldThrowError(DateTime ADateFrom, DateTime ADateTo, TestCaseDays ACase)
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(),
                GuestPhoneNumber = StringProvider.GetRandomString(9),
                BedroomsNumber = 1,
                DateFrom = ADateFrom,
                DateTo = ADateTo
            };

            // Act
            var LValidator = new AddBookingCommandValidator(FDateTimeService);
            var LResult = LValidator.Validate(LAddBookingCommand);

            // Assert
            if (ACase is TestCaseDays.Present or TestCaseDays.Future) 
            {
                LResult.Errors.Count.Should().Be(1);
                LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.START_DATE_AND_END_DATE_CANNOT_BE_SAME));
            }
            else
            {
                LResult.Errors.Count.Should().Be(3);
                LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.START_DATE_CANNOT_BE_EARLIER_THAN_TODAY));
                LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_TODAY));
                LResult.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.START_DATE_AND_END_DATE_CANNOT_BE_SAME));
            }
        }
        
        [Fact]
        public void GivenDateToIsEarlierThanDateFrom_WhenAddBooking_ShouldThrowError()
        {
            // Arrange
            var LAddBookingCommand = new AddBookingCommand
            {
                GuestFullName = StringProvider.GetRandomString(),
                GuestPhoneNumber = StringProvider.GetRandomString(9),
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
        
        private class DateTimeTestCases : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly DateTimeService FDateTimeService = new();
            
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { FDateTimeService.Now, FDateTimeService.Now, TestCaseDays.Present };
                yield return new object[] { FDateTimeService.Now.AddDays(-1), FDateTimeService.Now.AddDays(-1), TestCaseDays.Past };
                yield return new object[] { FDateTimeService.Now.AddDays(1), FDateTimeService.Now.AddDays(1), TestCaseDays.Future };
            }
        }
    }
}