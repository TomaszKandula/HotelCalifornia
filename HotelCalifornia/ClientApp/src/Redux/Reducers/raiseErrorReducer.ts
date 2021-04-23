import { Action, Reducer } from "@reduxjs/toolkit";
import { IRaiseError } from "../States/raiseErrorState";
import { combinedDefaults } from "../combinedDefaults";
import { CLEAR_ERROR, RAISE_ERROR, TErrorActions } from "../Actions/raiseErrorAction";
import { NO_ERRORS } from "../../Shared/constants";

export const RaiseErrorReducer: Reducer<IRaiseError> = (state: IRaiseError | undefined, incomingAction: Action): IRaiseError => 
{
    if (state === undefined) return combinedDefaults.raiseError;

    const action = incomingAction as TErrorActions;
    switch (action.type)
    {
        case CLEAR_ERROR:
            return {
                defaultErrorMessage: NO_ERRORS,
                attachedErrorObject: { }
            }

        case RAISE_ERROR:
            return {
                defaultErrorMessage: NO_ERRORS,
                attachedErrorObject: action.errorObject
            }

        default: return state;
    }
}
