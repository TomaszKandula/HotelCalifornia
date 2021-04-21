using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HotelCalifornia.Backend.Shared.Cors;
using HotelCalifornia.Backend.Shared.Settings;
using JetBrains.Annotations;

namespace HotelCalifornia.Middleware
{
    [UsedImplicitly]
    public class CustomCors
    {
        private readonly RequestDelegate FRequestDelegate;

        public CustomCors(RequestDelegate ARequestDelegate) => FRequestDelegate = ARequestDelegate;

        public Task Invoke(HttpContext AHttpContext, AppUrls AAppUrls)
        {
            var LDevelopmentOrigins = AAppUrls.DevelopmentOrigin.Split(';').ToList();
            var LDeploymentOrigins = AAppUrls.DeploymentOrigin.Split(';').ToList();
            var LRequestOrigin = AHttpContext.Request.Headers["Origin"];

            if (!LDevelopmentOrigins.Contains(LRequestOrigin) && !LDeploymentOrigins.Contains(LRequestOrigin))
                return FRequestDelegate(AHttpContext);
            
            CorsHeaders.Ensure(AHttpContext);

            // Necessary for pre-flight
            if (AHttpContext.Request.Method != "OPTIONS") 
                return FRequestDelegate(AHttpContext);
                
            AHttpContext.Response.StatusCode = 200;
            return AHttpContext.Response.WriteAsync("OK");
        }
    }
}