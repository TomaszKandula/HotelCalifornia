using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HotelCalifornia.Backend.Cqrs
{
    public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        protected TemplateHandler() { }

        public abstract Task<TResult> Handle(TRequest ARequest, CancellationToken ACancellationToken);
    }
}
