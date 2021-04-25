import { IAddBooking } from "./States/addBookingState";
import { IGetAllBookings } from "./States/getAllBookingsState";
import { IGetRoomsInfo } from "./States/getRoomsInfoState";
import { IRaiseDialog } from "./States/raiseDialogState";
import { IRaiseError } from "./States/raiseErrorState";
import { IRemoveBooking } from "./States/removeBookingState";

export interface IApplicationState 
{
    getAllBookings: IGetAllBookings;
    getRoomsInfo: IGetRoomsInfo;
    addBooking: IAddBooking;
    removeBooking: IRemoveBooking;
    raiseError: IRaiseError;
    raiseDialog: IRaiseDialog;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
