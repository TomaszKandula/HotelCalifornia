import { Action, Reducer } from "@reduxjs/toolkit";
import { IRemoveBooking } from "../States/removeBookingState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    REMOVE_BOOKING, 
    REMOVE_BOOKING_RESPONSE, 
    TKnownActions 
} from "../Actions/removeBookingAction";

export const RemoveBookingReducer: Reducer<IRemoveBooking> = (state: IRemoveBooking | undefined, incomingAction: Action): IRemoveBooking => 
{
    if (state === undefined) return combinedDefaults.removeBooking;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case REMOVE_BOOKING:
            return { isBookingRemoved: state.isBookingRemoved }

        case REMOVE_BOOKING_RESPONSE:
            return { isBookingRemoved: action.isBookingRemoved }

        default: return state;
    }
}
