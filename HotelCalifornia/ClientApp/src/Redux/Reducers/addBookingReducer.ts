import { Action, Reducer } from "@reduxjs/toolkit";
import { IAddBooking } from "../States/addBookingState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    ADD_BOOKING, 
    ADD_BOOKING_CLEAR, 
    ADD_BOOKING_ERROR,
    ADD_BOOKING_RESPONSE, 
    TKnownActions 
} from "../Actions/addBookingAction";
import { OperationStatus } from "../../Shared/enums";

export const AddBookingReducer: Reducer<IAddBooking> = (state: IAddBooking | undefined, incomingAction: Action): IAddBooking => 
{
    if (state === undefined) return combinedDefaults.addBooking;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case ADD_BOOKING_CLEAR:
            return combinedDefaults.addBooking;
        
        case ADD_BOOKING:
            return { 
                isAddingBooking: OperationStatus.inProgress,
                hasAddedBooking: state.hasAddedBooking 
            };

        case ADD_BOOKING_RESPONSE: 
            return { 
                isAddingBooking: OperationStatus.hasFinished,
                hasAddedBooking: action.hasAddedBooking 
            };

        case ADD_BOOKING_ERROR:
            return { 
                isAddingBooking: OperationStatus.hasFailed,
                hasAddedBooking: false 
            };

        default: return state;
    }
}
