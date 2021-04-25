import { AddBookingReducer } from "./Reducers/addBookingReducer";
import { GetAllBookingsReducer } from "./Reducers/getAllBookingsReducer";
import { GetRoomsInfoReducer } from "./Reducers/getRoomsInfoReducer";
import { RaiseErrorReducer } from "./Reducers/raiseErrorReducer";
import { RemoveBookingReducer } from "./Reducers/removeBookingReducer";

export const combinedReducers = 
{
    raiseError: RaiseErrorReducer,
    getAllBookings: GetAllBookingsReducer,
    getRoomsInfo: GetRoomsInfoReducer,
    addBooking: AddBookingReducer,
    removeBooking: RemoveBookingReducer
}