import { Action, Reducer } from "@reduxjs/toolkit";
import { IGetAllBookings } from "../States/getAllBookingsState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    REQUEST_BOOKINGS, 
    RECEIVE_BOOKINGS, 
    TKnownActions 
} from "../Actions/getAllBookingsAction";

export const GetAllBookingsReducer: Reducer<IGetAllBookings> = (state: IGetAllBookings | undefined, incomingAction: Action): IGetAllBookings => 
{
    if (state === undefined) return combinedDefaults.getAllBookings;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case REQUEST_BOOKINGS:
            return { 
                isLoading: true,
                bookings: state.bookings
            }

        case RECEIVE_BOOKINGS:
            return { 
                isLoading: false,
                bookings: action.payload
            }

        default: return state;
    }
}
