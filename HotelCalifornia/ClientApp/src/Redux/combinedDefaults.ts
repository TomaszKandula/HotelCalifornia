import { AddBookingDefault } from "./Defaults/addBookingDefault";
import { GetAllBookingsDefault } from "./Defaults/getAllBookingsDefault";
import { GetRoomsInfoDefault } from "./Defaults/getRoomsInfoDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RemoveBookingDefault } from "./Defaults/removeBookingDefault";

export const combinedDefaults = 
{
    getAllBookings: GetAllBookingsDefault,
    getRoomsInfo: GetRoomsInfoDefault,
    addBooking: AddBookingDefault,
    removeBooking: RemoveBookingDefault,
    raiseError: RaiseErrorDefault
}