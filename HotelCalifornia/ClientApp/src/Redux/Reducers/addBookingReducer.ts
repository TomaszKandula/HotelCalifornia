import { Action, Reducer } from "@reduxjs/toolkit";
import { IAddBooking } from "../States/addBookingState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    ADD_BOOKING, 
    ADD_BOOKING_CLEAR, 
    ADD_BOOKING_RESPONSE, 
    TKnownActions 
} from "../Actions/addBookingAction";

export const AddBookingReducer: Reducer<IAddBooking> = (state: IAddBooking | undefined, incomingAction: Action): IAddBooking => 
{
    if (state === undefined) return combinedDefaults.addBooking;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case ADD_BOOKING_CLEAR:
            return combinedDefaults.addBooking;
        
        case ADD_BOOKING:
            return { hasAddedBooking: state.hasAddedBooking };

        case ADD_BOOKING_RESPONSE: 
            return { hasAddedBooking: action.hasAddedBooking };

        default: return state;
    }
}
