using System.Threading;
using System.Threading.Tasks;
using HotelCalifornia.Backend.Core.Services.AppLogger;
using MediatR;

namespace HotelCalifornia.Backend.Core.Behaviours
{   
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAppLogger FAppLogger;

        public LoggingBehaviour(IAppLogger AAppLogger) => FAppLogger = AAppLogger;

        public async Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
        {
            FAppLogger.LogInfo($"Begin: Handle {typeof(TRequest).Name}");
            var LResponse = await ANext();
            FAppLogger.LogInfo($"Finish: Handle {typeof(TResponse).Name}");
            return LResponse;
        }
    }
}
