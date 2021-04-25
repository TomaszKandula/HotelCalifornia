import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as ActionCreatorsBooking } from "../Redux/Actions/addBookingAction";
import { ActionCreators as ActionCreatorsDialog } from "../Redux/Actions/raiseDialogAction";
import { IAddBookingDto } from "../Models";
import { BookingError, BookingSuccess, BookingWarning } from "../Shared/textWrappers";
import { IconType, OperationStatus } from "../Shared/enums";
import { ValidateBookingForm } from "../Shared/validate";
import { UserPageView } from "./userPageView";
import Validate from "validate.js";

const formDefaultValues: IAddBookingDto = 
{
    GuestFullName: "",
    GuestPhoneNumber: "",
    BedroomsNumber: 0,
    DateFrom: "",
    DateTo: ""
}

export default function UserPage() 
{
    const [progress, setProgress] = React.useState(false);
    const [form, setForm] = React.useState(formDefaultValues);

    const dispatch = useDispatch();
    const addBookingState = useSelector((state: IApplicationState) => state.addBooking);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const showSuccess = React.useCallback((text: string) => 
    { dispatch(ActionCreatorsDialog.raiseDialog({ type: IconType.info, title: "Info", message: text, isShown: true }))}, [ dispatch ]);
    
    const showWarning = React.useCallback((text: string) =>  
    { dispatch(ActionCreatorsDialog.raiseDialog({ type: IconType.warning, title: "Warning", message: text, isShown: true })) }, [ dispatch ]);
    
    const showError = React.useCallback((text: string) => 
    { dispatch(ActionCreatorsDialog.raiseDialog({ type: IconType.error, title: "Error", message: text, isShown: true })) }, [ dispatch ]);

    const addBooking = React.useCallback((payload: IAddBookingDto) => 
    { dispatch(ActionCreatorsBooking.addBooking(payload)); }, [ dispatch ]);

    const addBookingClear = React.useCallback(() => 
    { dispatch(ActionCreatorsBooking.addBookingClear()); }, [ dispatch ]);

    React.useEffect(() => 
    {
        if (addBookingState === undefined) return;
        if (addBookingState.isAddingBooking === OperationStatus.notStarted && progress)
        {
            addBooking(
            { 
                GuestFullName: form.GuestFullName,
                GuestPhoneNumber: form.GuestPhoneNumber,
                BedroomsNumber: form.BedroomsNumber,
                DateFrom: form.DateFrom,
                DateTo: form.DateTo
            });
            return;
        }

        if (addBookingState.isAddingBooking === OperationStatus.hasFinished 
            || addBookingState.isAddingBooking === OperationStatus.hasFailed)
        {
            setProgress(false);
            setForm(formDefaultValues);

            if (addBookingState.hasAddedBooking 
                && addBookingState.isAddingBooking === OperationStatus.hasFinished)
            {
                showSuccess(BookingSuccess());
                addBookingClear();
                return;
            }

            showError(BookingError(raiseErrorState.attachedErrorObject));
            addBookingClear();
        }       
    }, 
    [ addBooking, addBookingClear, addBookingState, form, progress ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); };

    const buttonHandler = () => 
    {
        let results = ValidateBookingForm(form);
        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(BookingWarning(results));
    };

    return(<UserPageView bind={ 
    { 
        formHandler: formHandler,
        buttonHandler: buttonHandler,
        guestFullName: form.GuestFullName,
        guestPhoneNumber: form.GuestPhoneNumber,
        bedroomsNumber: form.BedroomsNumber,
        dateFrom: form.DateFrom,
        dateTo: form.DateTo
    }}/>);
}
