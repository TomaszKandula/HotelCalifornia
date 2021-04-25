using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotelCalifornia.Backend.Cqrs.Handlers.Queries.Booking;
using MediatR;

namespace HotelCalifornia.Controllers
{
    public class RoomController : __BaseController
    {
        public RoomController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        public async Task<IEnumerable<GetRoomsInfoQueryResult>> GetRoomsInfo()
            => await FMediator.Send(new GetRoomsInfoQuery());
    }
}