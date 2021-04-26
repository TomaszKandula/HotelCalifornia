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
                var LBedroomSuffix = string.Empty;
                var LRoomSuffix = string.Empty;
                
                if (LQueryResult.Bedrooms > 1)
                    LBedroomSuffix = PLURAL_SUFFIX;
                
                if (LQueryResult.TotalRooms > 1)
                    LRoomSuffix = PLURAL_SUFFIX;

                LRoomsInfo.Add(new GetRoomsInfoQueryResult
                {
                    Id = Guid.NewGuid(),
                    Info = $"{LQueryResult.TotalRooms} room{LRoomSuffix} with {LQueryResult.Bedrooms} bedroom{LBedroomSuffix}."
                });
            }

            return await Task.FromResult(LRoomsInfo);
        }
    }
}