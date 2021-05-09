using System;

namespace HotelCalifornia.Backend.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }

        public BusinessException(string AErrorCode, string AErrorMessage = "") : base(AErrorMessage)
        {
            ErrorCode = AErrorCode;
        }
    }
}
