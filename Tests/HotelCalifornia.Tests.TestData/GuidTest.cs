using System;

namespace HotelCalifornia.Tests.TestData
{
    public static class GuidTest
    {
        public static bool Check(string AValue) 
        {
            var LValue = AValue.Replace("\"", "");
            return Guid.TryParse(LValue, out var _);
        }
    }
}
