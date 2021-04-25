using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelCalifornia.Backend.Database;

namespace HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking
{
    public class GetRoomsInfoQueryHandler : TemplateHandler<GetRoomsInfoQuery, IEnumerable<GetRoomsInfoQueryResult>>
    {
        private const string PLURAL_SUFFIX = "s";
        private readonly DatabaseContext FDatabaseContext;

        public GetRoomsInfoQueryHandler(DatabaseContext ADatabaseContext)
            => FDatabaseContext = ADatabaseContext;

        public override async Task<IEnumerable<GetRoomsInfoQueryResult>> Handle(GetRoomsInfoQuery ARequest, CancellationToken ACancellationToken)
        {
            var LQueryResults =
                from LRooms in FDatabaseContext.Rooms
                group LRooms by LRooms.Bedrooms
                into LGrouping
                select new
                {
                    Bedrooms = LGrouping.Key,
                    TotalRooms = LGrouping.Select(ARooms => ARooms.Bedrooms).Count()
                };

            var LRoomsInfo = new List<GetRoomsInfoQueryResult>();
            foreach (var LQueryResult in LQueryResults)
            {
                var LSuffix = "";
                if (LQueryResult.Bedrooms > 1)
                    LSuffix = PLURAL_SUFFIX;
                
                LRoomsInfo.Add(new GetRoomsInfoQueryResult
                {
                    Id = Guid.NewGuid(),
                    Info = $"{LQueryResult.TotalRooms} rooms with {LQueryResult.Bedrooms} bedroom{LSuffix}."
                });
            }

            return await Task.FromResult(LRoomsInfo);
        }
    }
}