using System.IO;

namespace HotelCalifornia.Backend.Core.Generators
{
    public abstract class StreamProvider : BaseClass
    {
        public static MemoryStream GetRandom(int ASizeInKb = 12)
            => new (GetRandomByteArray(ASizeInKb));
        
        private static byte[] GetRandomByteArray(int ASizeInKb = 12)
        {
            var LByteBuffer = new byte[ASizeInKb * 1024]; 
            Random.NextBytes(LByteBuffer); 
            return LByteBuffer;
        }
    }
}