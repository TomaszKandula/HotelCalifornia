﻿using System.Linq;
using System.Collections.Generic;
using FluentValidation.Results;

namespace HotelCalifornia.Backend.Core.Models
{
    public sealed class ApplicationError
    {
        public string ErrorMessage { get; set; }

        public string ErrorCode { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApplicationError(string AErrorCode, string AErrorMessage)
        {
            ErrorCode = AErrorCode;
            ErrorMessage = AErrorMessage;
        }

        public ApplicationError(string AErrorCode, string AErrorMessage, ValidationResult AValidationResult) : this(AErrorCode, AErrorMessage)
        {
            ValidationErrors = AValidationResult.Errors
                .Select(AValidationFailure => new ValidationError(AValidationFailure))
                .ToList();
        }
    }
}
