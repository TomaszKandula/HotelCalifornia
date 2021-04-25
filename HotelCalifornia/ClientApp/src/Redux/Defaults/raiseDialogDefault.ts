import { IconType } from "../../Shared/enums";
import { IRaiseDialog } from "../../Redux/States/raiseDialogState";

export const RaiseDialogDefault: IRaiseDialog = 
{
    type: IconType.default,
    title: "",
    message: "",
    isShown: false
}