using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using JetBrains.Annotations;

namespace HotelCalifornia.Middleware
{
    /// <summary>
    /// This middleware class will provide a GC.Collect for 2nd generation 
    /// after every endpoint call. It shall improve memory utilisation 
    /// in Azure App Service (release large objects faster).
    /// </summary>
    [UsedImplicitly]
    public class GarbageCollector
    {
        private readonly RequestDelegate FRequestDelegate;

        public GarbageCollector(RequestDelegate ARequestDelegate) => FRequestDelegate = ARequestDelegate;

        public async Task Invoke(HttpContext AHttpContext)
        {
            await FRequestDelegate(AHttpContext);
            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
        }    
    }
}