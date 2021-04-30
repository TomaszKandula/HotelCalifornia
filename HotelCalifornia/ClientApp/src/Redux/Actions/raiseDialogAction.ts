import { AppThunkAction } from "../applicationState";
import { IRaiseDialog } from "../States/raiseDialogState";

export const CLEAR_DIALOG_BOX = "CLEAR_DIALOG_BOX";
export const RAISE_DIALOG_BOX = "RAISE_DIALOG_BOX";

export interface IClearDialogBox { type: typeof CLEAR_DIALOG_BOX }
export interface IRaiseDialogBox { type: typeof RAISE_DIALOG_BOX, dialog: IRaiseDialog }

export type TKnownActions = IClearDialogBox | IRaiseDialogBox;

export const ActionCreators = 
{
    clearDialog: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_DIALOG_BOX });
    },
    raiseDialog: (dialog: IRaiseDialog): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RAISE_DIALOG_BOX, dialog: dialog });
    }
}