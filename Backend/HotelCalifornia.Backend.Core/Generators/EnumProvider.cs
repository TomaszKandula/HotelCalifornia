using System;

namespace HotelCalifornia.Backend.Core.Generators
{
    public abstract class EnumProvider : BaseClass
    {
        public static T GetRandom<T>()
        {
            var LRandom = new Random(); 
            var LValues = Enum.GetValues(typeof(T)); 
            return (T)LValues.GetValue(LRandom.Next(LValues.Length));
        }
    }
}