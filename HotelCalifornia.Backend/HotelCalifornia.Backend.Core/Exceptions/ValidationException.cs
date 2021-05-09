﻿using FluentValidation.Results;

namespace HotelCalifornia.Backend.Core.Exceptions
{
    public class ValidationException : BusinessException
    {
        public ValidationResult ValidationResult { get; }

        public ValidationException(ValidationResult AValidationResult, string AErrorMessage = "") : base(AErrorMessage)
            => ValidationResult = AValidationResult;
    }
}
