import { AddBookingReducer } from "./Reducers/addBookingReducer";
import { GetAllBookingsReducer } from "./Reducers/getAllBookingsReducer";
import { RaiseErrorReducer } from "./Reducers/raiseErrorReducer";
import { RemoveBookingReducer } from "./Reducers/removeBookingReducer";

export const combinedReducers = 
{
    raiseError: RaiseErrorReducer,
    getAllBookings: GetAllBookingsReducer,
    addBooking: AddBookingReducer,
    removeBooking: RemoveBookingReducer
}