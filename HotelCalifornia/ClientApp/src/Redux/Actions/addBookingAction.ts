import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAddBookingDto } from "../../Models";
import { API_COMMAND_ADD_BOOKING } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const ADD_BOOKING = "ADD_BOOKING";
export const ADD_BOOKING_CLEAR = "ADD_BOOKING_CLEAR";
export const ADD_BOOKING_ERROR = "ADD_BOOKING_ERROR";
export const ADD_BOOKING_RESPONSE = "ADD_BOOKING_RESPONSE";

export interface IAddBooking { type: typeof ADD_BOOKING }
export interface IAddBookingClear { type: typeof ADD_BOOKING_CLEAR }
export interface IAddBookingError { type: typeof ADD_BOOKING_ERROR }
export interface IAddBookingResponse { type: typeof ADD_BOOKING_RESPONSE, hasAddedBooking: boolean }

export type TKnownActions = 
    IAddBooking | 
    IAddBookingClear | 
    IAddBookingError |
    IAddBookingResponse | 
    TErrorActions
;

export const ActionCreators = 
{
    addBookingClear: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: ADD_BOOKING_CLEAR });
    },    
    addBooking: (payload: IAddBookingDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: ADD_BOOKING });
        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_BOOKING, 
            data: 
            { 
                guestFullName: payload.GuestFullName,
                guestPhoneNumber: payload.GuestPhoneNumber,
                bedroomsNumber: payload.BedroomsNumber,
                dateFrom: payload.DateFrom,
                dateTo: payload.DateTo
            }
        })
        .then(response => 
        {
            return response.status === 200 
                ? dispatch({ type: ADD_BOOKING_RESPONSE, hasAddedBooking: true })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error => 
        {
            dispatch({ type: ADD_BOOKING_ERROR });
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        });
    }
}