import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IBookings } from "../States/getAllBookingsState";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { API_QUERY_GET_BOOKINGS } from "../../Shared/constants";

export const RESET_SELECTION = "RESET_SELECTION";
export const REQUEST_BOOKINGS = "REQUEST_BOOKINGS";
export const RECEIVE_BOOKINGS = "RECEIVE_BOOKINGS";

export interface IResetSelection { type: typeof RESET_SELECTION; }
export interface IRequestBookings { type: typeof REQUEST_BOOKINGS }
export interface IReceiveBookings { type: typeof RECEIVE_BOOKINGS, payload: IBookings[] }

export type TKnownActions = 
    IRequestBookings | 
    IReceiveBookings | 
    IResetSelection |
    TErrorActions
;

export const ActionCreators = 
{
    resetSelection: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET_SELECTION });
    },
    requestBooking: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: REQUEST_BOOKINGS });
        await axios(
        { 
            method: "GET", 
            url: API_QUERY_GET_BOOKINGS
        })
        .then(response => 
        {
            return response.status === 200 
                ? dispatch({ type: RECEIVE_BOOKINGS, payload: response.data })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error => 
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        });
    }
}