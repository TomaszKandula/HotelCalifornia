import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IRemoveBookingDto } from "../../Models";
import { API_COMMAND_REMOVE_BOOKING } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const REMOVE_BOOKING = "REMOVE_BOOKING";
export const REMOVE_BOOKING_RESPONSE = "REMOVE_BOOKING_RESPONSE";

export interface IRemoveBooking { type: typeof REMOVE_BOOKING }
export interface IRemoveBookingResponse { type: typeof REMOVE_BOOKING_RESPONSE, isBookingRemoved: boolean }

export type TKnownActions = 
    IRemoveBooking |  
    IRemoveBookingResponse | 
    TErrorActions
;

export const ActionCreators = 
{
    removeBooking: (payload: IRemoveBookingDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: REMOVE_BOOKING });
        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_BOOKING, 
            data: { bookingId: payload.BookingId }
        })
        .then(response => 
        {
            return response.status === 200 
                ? dispatch({ type: REMOVE_BOOKING_RESPONSE, isBookingRemoved: true })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error => 
        {
            dispatch({ type: RAISE_ERROR, errorObject: error });
        });
    }
}