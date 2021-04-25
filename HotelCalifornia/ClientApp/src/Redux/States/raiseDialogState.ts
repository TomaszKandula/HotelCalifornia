import { IconType } from "../../Shared/enums";

export interface IRaiseDialog
{
    type: IconType;
    title: string;
    message: string;
    isShown: boolean;
}