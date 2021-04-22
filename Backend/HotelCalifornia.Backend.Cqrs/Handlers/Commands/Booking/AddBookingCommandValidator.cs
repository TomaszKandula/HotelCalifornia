using FluentValidation;
using HotelCalifornia.Backend.Core.Models;
using HotelCalifornia.Backend.Core.Extensions;
using HotelCalifornia.Backend.Shared.Resources;
using HotelCalifornia.Backend.Core.Services.DateTimeService;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking
{
    public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator(IDateTimeService ADateTimeService)
        {
            RuleFor(AAddBookingCommand => AAddBookingCommand.GuestFullName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.GUEST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.GUEST_NAME_TOO_LONG);
            
            RuleFor(AAddBookingCommand => AAddBookingCommand.GuestPhoneNumber)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(12)
                .WithErrorCode(nameof(ValidationCodes.PHONE_NUMBER_TOO_LONG))
                .WithMessage(ValidationCodes.PHONE_NUMBER_TOO_LONG);

            RuleFor(AAddBookingCommand => AAddBookingCommand.BedroomsNumber)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .GreaterThan(0)
                .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                .WithMessage(ValidationCodes.LESS_THAN_ZERO);

            RuleFor(AAddBookingCommand => AAddBookingCommand.DateFrom)
                .NotEmpty()
                .IsSameOrLaterThanDate(ADateTimeService.TodayStartOfDay)
                .WithErrorCode(nameof(ValidationCodes.START_DATE_CANNOT_BE_EARLIER_THAN_TODAY))
                .WithMessage(ValidationCodes.START_DATE_CANNOT_BE_EARLIER_THAN_TODAY);

            RuleFor(AAddBookingCommand => AAddBookingCommand.DateTo)
                .IsSameOrLaterThanDate(ADateTimeService.TodayStartOfDay)
                .WithErrorCode(nameof(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_TODAY))
                .WithMessage(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_TODAY);

            RuleFor(AAddBookingCommand => new DateRangeValidator(AAddBookingCommand.DateFrom, AAddBookingCommand.DateTo))
                .IsValidDateRange()
                .WithName(AAddBookingCommand => nameof(AAddBookingCommand.DateTo))
                .WithErrorCode(nameof(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_STAR_DATE))
                .WithMessage(ValidationCodes.END_DATE_CANNOT_BE_EARLIER_THAN_STAR_DATE);
            
            RuleFor(AAddBookingCommand => new DateRangeValidator(AAddBookingCommand.DateFrom, AAddBookingCommand.DateTo))
                .IsSameDate()
                .WithName(AAddBookingCommand => nameof(AAddBookingCommand.DateTo))
                .WithErrorCode(nameof(ValidationCodes.START_DATE_AND_END_DATE_CANNOT_BE_SAME))
                .WithMessage(ValidationCodes.START_DATE_AND_END_DATE_CANNOT_BE_SAME);
        }
    }
}
