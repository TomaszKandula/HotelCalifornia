using System;

namespace HotelCalifornia.Backend.Core.Services.DateTimeService
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime TodayStartOfDay { get; }
        DateTime TodayEndOfDay { get; }
        DateTime GetStartOfDay(DateTime AValue);
        DateTime GetEndOfDay(DateTime AValue);
        DateTime GetFirstDayOfMonth(DateTime AValue);
    }
}