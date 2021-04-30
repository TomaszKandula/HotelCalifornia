import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IRoomsInfo } from "../../Redux/States/getRoomsInfoState";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { API_QUERY_GET_ROOMS } from "../../Shared/constants";

export const REQUEST_ROOMSINFO = "REQUEST_ROOMSINFO";
export const RECEIVE_ROOMSINFO = "RECEIVE_ROOMSINFO";

export interface IRequestRoomsInfo { type: typeof REQUEST_ROOMSINFO }
export interface IReceiveRoomsInfo { type: typeof RECEIVE_ROOMSINFO, payload: IRoomsInfo[] }

export type TKnownActions = 
    IRequestRoomsInfo | 
    IReceiveRoomsInfo | 
    TErrorActions
;

export const ActionCreators = 
{
    requestRoomsInfo: ():  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REQUEST_ROOMSINFO });
        axios(
        { 
            method: "GET", 
            url: API_QUERY_GET_ROOMS
        })
        .then(response => 
        {
            return response.status === 200 
                ? dispatch({ type: RECEIVE_ROOMSINFO, payload: response.data })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error => 
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        });
    }
}