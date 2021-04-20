using MediatR;

namespace HotelCalifornia.Controllers
{
    public class BookingController : __BaseController
    {
        public BookingController(IMediator AMediator) : base(AMediator) { }
    }
}