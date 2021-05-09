using Xunit;
using FluentAssertions;
using System;
using HotelCalifornia.Backend.Shared.Resources;
using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;

namespace HotelCalifornia.UnitTests.Validators
{
    public class RemoveBookingCommandHandlerTest
    {
        [Fact]
        public void GivenCorrectId_WhenRemoveArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LRemoveBookingCommand = new RemoveBookingCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new RemoveBookingCommandValidator();
            var LResult = LValidator.Validate(LRemoveBookingCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
        {
            // Arrange
            var LRemoveArticleCommand = new RemoveBookingCommand
            {
                Id = Guid.Empty
            };
        
            // Act
            var LValidator = new RemoveBookingCommandValidator();
            var LResult = LValidator.Validate(LRemoveArticleCommand);
        
            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }        
    }
}