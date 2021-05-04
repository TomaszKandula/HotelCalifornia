using System;

namespace HotelCalifornia.Tests.DataProviders
{
    public abstract class DateTimeProvider : BaseClass
    {
        public static DateTime GetRandom(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020)
        {
            AMin ??= new DateTime(ADefaultYear, 1, 1); 
            AMax ??= new DateTime(ADefaultYear, 12, 31); 

            var LDayRange = (AMax - AMin).Value.Days; 

            return AMin.Value.AddDays(FRandom.Next(0, LDayRange));
        }
    }
}