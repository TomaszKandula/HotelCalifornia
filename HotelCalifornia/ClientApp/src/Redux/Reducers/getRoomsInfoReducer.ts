import { Action, Reducer } from "@reduxjs/toolkit";
import { IGetRoomsInfo } from "../../Redux/States/getRoomsInfoState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    REQUEST_ROOMSINFO, 
    RECEIVE_ROOMSINFO, 
    TKnownActions 
} from "../Actions/getRoomsInfoAction";

export const GetRoomsInfoReducer: Reducer<IGetRoomsInfo> = (state: IGetRoomsInfo | undefined, incomingAction: Action): IGetRoomsInfo => 
{
    if (state === undefined) return combinedDefaults.getRoomsInfo;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case REQUEST_ROOMSINFO:
            return { 
                isLoading: true,
                roomsInfo: state.roomsInfo
            }

        case RECEIVE_ROOMSINFO:
            return { 
                isLoading: false,
                roomsInfo: action.payload
            }

        default: return state;
    }
}
