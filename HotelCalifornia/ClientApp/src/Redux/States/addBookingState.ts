import { OperationStatus } from "../../Shared/enums";

export interface IAddBooking
{
    isAddingBooking: OperationStatus;
    hasAddedBooking: boolean;
    roomNumber: number;
}