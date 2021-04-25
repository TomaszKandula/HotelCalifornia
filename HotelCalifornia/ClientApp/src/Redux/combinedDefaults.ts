import { AddBookingDefault } from "./Defaults/addBookingDefault";
import { GetAllBookingsDefault } from "./Defaults/getAllBookingsDefault";
import { GetRoomsInfoDefault } from "./Defaults/getRoomsInfoDefault";
import { RemoveBookingDefault } from "./Defaults/removeBookingDefault";
import { RaiseDialogDefault } from "./Defaults/raiseDialogDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";

export const combinedDefaults = 
{
    getAllBookings: GetAllBookingsDefault,
    getRoomsInfo: GetRoomsInfoDefault,
    addBooking: AddBookingDefault,
    removeBooking: RemoveBookingDefault,
    raiseError: RaiseErrorDefault,
    raiseDialog: RaiseDialogDefault
}