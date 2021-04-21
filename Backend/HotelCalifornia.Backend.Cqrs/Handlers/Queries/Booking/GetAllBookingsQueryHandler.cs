using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetAllArticlesQueryHandler : TemplateHandler<GetAllBookingsQuery, IEnumerable<GetAllBookingsQueryResult>>
    {
        public GetAllArticlesQueryHandler() { }

        public override async Task<IEnumerable<GetAllBookingsQueryResult>> Handle(GetAllBookingsQuery ARequest, CancellationToken ACancellationToken)
        {

            return new List<GetAllBookingsQueryResult>();
        }
    }
}
