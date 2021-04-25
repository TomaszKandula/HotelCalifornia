import { AddBookingReducer } from "./Reducers/addBookingReducer";
import { GetAllBookingsReducer } from "./Reducers/getAllBookingsReducer";
import { GetRoomsInfoReducer } from "./Reducers/getRoomsInfoReducer";
import { RaiseDialogReducer } from "./Reducers/raiseDialogReducer";
import { RaiseErrorReducer } from "./Reducers/raiseErrorReducer";
import { RemoveBookingReducer } from "./Reducers/removeBookingReducer";

export const combinedReducers = 
{
    getAllBookings: GetAllBookingsReducer,
    getRoomsInfo: GetRoomsInfoReducer,
    addBooking: AddBookingReducer,
    removeBooking: RemoveBookingReducer,
    raiseError: RaiseErrorReducer,
    raiseDialog: RaiseDialogReducer
}