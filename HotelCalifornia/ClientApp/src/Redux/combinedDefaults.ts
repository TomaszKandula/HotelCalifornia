import { AddBookingDefault } from "./Defaults/addBookingDefault";
import { GetAllBookingsDefault } from "./Defaults/getAllBookingsDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RemoveBookingDefault } from "./Defaults/removeBookingDefault";

export const combinedDefaults = 
{
    getAllBookings: GetAllBookingsDefault,
    addBooking: AddBookingDefault,
    removeBooking: RemoveBookingDefault,
    raiseError: RaiseErrorDefault
}