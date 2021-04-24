import { IAddBooking } from "../States/addBookingState";
import { OperationStatus } from "../../Shared/enums";

export const AddBookingDefault: IAddBooking = 
{
    isAddingBooking: OperationStatus.notStarted,
    hasAddedBooking: false
}