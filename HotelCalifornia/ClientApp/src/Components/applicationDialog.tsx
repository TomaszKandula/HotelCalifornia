import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../Redux/Actions/raiseDialogAction";
import { IApplicationState } from "../Redux/applicationState";
import { AlertDialog } from "./alertDialog";

export const ApplicationDialog = () => 
{
    const [state, setState] = React.useState(false); 
    const dispatch = useDispatch();

    const raiseDialogState = useSelector((state: IApplicationState) => state.raiseDialog);
    const clearDialog = React.useCallback(() => dispatch(ActionCreators.clearDialog()), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (raiseDialogState === undefined) return;
        if (raiseDialogState.message !== "") setState(true); 
    }, 
    [ raiseDialogState ]);

    React.useEffect(() => 
    { 
        if (!state) clearDialog();
    }, 
    [ state ]);

    const buttonHandler = () => 
    {
        setState(false);
    };

    return (
        <>
            <AlertDialog 
                state={state} 
                handle={buttonHandler} 
                title={raiseDialogState?.title} 
                message={raiseDialogState?.message} 
                icon={raiseDialogState?.type} 
            />
        </>
    );
}
