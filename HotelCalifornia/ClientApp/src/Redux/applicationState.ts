import { IAddBooking } from "./States/addBookingState";
import { IGetAllBookings } from "./States/getAllBookingsState";
import { IRaiseError } from "./States/raiseErrorState";
import { IRemoveBooking } from "./States/removeBookingState";

export interface IApplicationState 
{
    getAllBookings: IGetAllBookings;
    addBooking: IAddBooking;
    removeBooking: IRemoveBooking;
    raiseError: IRaiseError;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
