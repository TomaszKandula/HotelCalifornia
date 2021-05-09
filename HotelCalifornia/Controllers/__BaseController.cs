using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class __BaseController : ControllerBase
    {
        protected readonly IMediator FMediator;
        
        public __BaseController(IMediator AMediator) => FMediator = AMediator;
    }
}