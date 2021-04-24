import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators } from "../Redux/Actions/addBookingAction";
import { IAddBookingDto } from "../Models";
import { BookingError, BookingSuccess, BookingWarning } from "../Shared/textWrappers";
import { IconType, OperationStatus } from "../Shared/enums";
import { ValidateBookingForm } from "../Shared/validate";
import { alertModalDefault } from "../Components/alertDialog";
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
    const [modal, setModal] = React.useState(alertModalDefault);
    const [form, setForm] = React.useState(formDefaultValues);

    const showSuccess = (text: string) => { setModal({ state: true, title: "Booking", message: text, icon: IconType.info }); };
    const showWarning = (text: string) => { setModal({ state: true, title: "Warning", message: text, icon: IconType.warning }); };
    const showError = (text: string) => { setModal({ state: true, title: "Error", message: text, icon: IconType.error }); };
        
    const addBookingState = useSelector((state: IApplicationState) => state.addBooking);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const dispatch = useDispatch();

    const addBooking = React.useCallback((payload: IAddBookingDto) => 
    { dispatch(ActionCreators.addBooking(payload)); }, [ dispatch ]);

    const addBookingClear = React.useCallback(() => 
    { dispatch(ActionCreators.addBookingClear()); }, [ dispatch ]);

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
                return;
            }

            showError(BookingError(raiseErrorState.attachedErrorObject));
        }       
    }, 
    [ addBooking, addBookingClear, addBookingState, form, progress ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); };

    const modalHandler = () => 
    { 
        addBookingClear();
        setModal({ ...modal, state: false }); 
    };

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

    return(<UserPageView  
        state={modal.state}
        handle={modalHandler}
        title={modal.title} 
        message={modal.message} 
        icon={modal.icon}
        formHandler={formHandler}
        buttonHandler={buttonHandler}
        guestFullName={form.GuestFullName}
        guestPhoneNumber={form.GuestPhoneNumber}
        bedroomsNumber={form.BedroomsNumber}
        dateFrom={form.DateFrom}
        dateTo={form.DateTo}
    />);
}
