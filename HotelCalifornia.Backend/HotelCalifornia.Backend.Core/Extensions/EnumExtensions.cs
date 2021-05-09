using System;
using System.Linq;
using System.ComponentModel;

namespace HotelCalifornia.Backend.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum AValue)
        {
            var LField = AValue.GetType().GetField(AValue.ToString());

            if (LField?.GetCustomAttributes(typeof(DescriptionAttribute), false) 
                is DescriptionAttribute[] LAttributes && LAttributes.Any())
                return LAttributes.First().Description;

            return AValue.ToString();
        }
    }
}