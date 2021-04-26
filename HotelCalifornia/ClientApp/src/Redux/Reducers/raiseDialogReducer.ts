import { Action, Reducer } from "@reduxjs/toolkit";
import { IRaiseDialog } from "../../Redux/States/raiseDialogState";
import { combinedDefaults } from "../combinedDefaults";
import { 
    CLEAR_DIALOG_BOX, 
    RAISE_DIALOG_BOX, 
    TKnownActions 
} from "../../Redux/Actions/raiseDialogAction";

export const RaiseDialogReducer: Reducer<IRaiseDialog> = (state: IRaiseDialog | undefined, incomingAction: Action): IRaiseDialog => 
{
    if (state === undefined) return combinedDefaults.raiseDialog;

    const action = incomingAction as TKnownActions;
    switch (action.type)
    {
        case CLEAR_DIALOG_BOX:
            return combinedDefaults.raiseDialog;

        case RAISE_DIALOG_BOX:
            return {
                type: action.dialog.type,
                title: action.dialog.title,
                message: action.dialog.message
            }

        default: return state;
    }
}
