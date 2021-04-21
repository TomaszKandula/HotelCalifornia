using HotelCalifornia.Backend.Cqrs.Handlers.Commands.Booking;
using HotelCalifornia.Backend.Shared.Dto.Booking;

namespace HotelCalifornia.Backend.Cqrs.Mappers
{
    public static class BookingMapper
    {
        public static AddBookingCommand MapToAddBookingCommand(AddBookingDto AModel)
        {
            return new AddBookingCommand
            {
                GuestFullName = AModel.GuestFullName,
                GuestPhoneNumber = AModel.GuestPhoneNumber,
                BedroomsNumber = AModel.BedroomsNumber,
                DateFrom = AModel.DateFrom,
                DateTo = AModel.DateTo
            };
        }
        
        public static RemoveBookingCommand MapToRemoveBookingCommand(RemoveBookingDto AModel) 
        {
            return new RemoveBookingCommand 
            { 
                Id = AModel.BookingId
            };
        }
    }
}
