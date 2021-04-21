using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotelCalifornia.Backend.Shared.Dto.Booking;
using MediatR;

namespace HotelCalifornia.Controllers
{
    public class BookingController : __BaseController
    {
        public BookingController(IMediator AMediator) : base(AMediator) { }
        
        [HttpGet]
        public async Task<IEnumerable<GetAllBookingsQueryResult>> GetAllBookings()
            => await FMediator.Send(new GetAllBookingsQuery());
        
        [HttpPost]
        public async Task<Guid> AddBooking([FromBody] AddBookingDto APayLoad)
            =>  await FMediator.Send(BookingMapper.MapToAddBookingCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> RemoveBooking([FromBody] RemoveBookingDto APayLoad)
            => await FMediator.Send(BookingMapper.MapToRemoveUserCommand(APayLoad));
    }
}