import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { NOT_APPLICABLE, REDIRECT_TO_MAIN } from "../Shared/constants";
import { ActionCreators as ActionCreatorsRequest } from "../Redux/Actions/getAllBookingsAction";
import { ActionCreators as ActionCreatorsRemove } from "../Redux/Actions/removeBookingAction";
import { IApplicationState } from "../Redux/applicationState";
import { ManagerPageView } from "./managerPageView";

export default function ManagerPage() 
{
    const [selection, setSelection] = React.useState(NOT_APPLICABLE);
    const [reload, setReload] = React.useState(false);
    const [remove, setRemove] = React.useState(false);

    const data = useSelector((state: IApplicationState) => state.getAllBookings);
    const history = useHistory();
    const dispatch = useDispatch();

    const fetchData = React.useCallback(() => 
    dispatch(ActionCreatorsRequest.requestBooking()), [ dispatch ]);
 
    const deleteBooking = React.useCallback((bookingId: string) => 
    dispatch(ActionCreatorsRemove.removeBooking({ BookingId: bookingId })), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (!data.isLoading && data.bookings.length === 0) fetchData(); 
    }, 
    [ fetchData ]);

    React.useEffect(() => 
    { 
        if (reload) fetchData();
        setReload(false);
    }, 
    [ fetchData, reload ]);

    React.useEffect(() => 
    { 
        if (remove && selection !== NOT_APPLICABLE) 
        {
            deleteBooking(selection);
            setSelection(NOT_APPLICABLE);
        }
        
        setRemove(false);
    }, 
    [ deleteBooking, remove ]);
    
    const backButton = () => history.push(REDIRECT_TO_MAIN);
    const refreshButton = () => setReload(true);
    const removeButton = () => setRemove(true);

    const selectEvent = (event: React.MouseEvent<HTMLElement, MouseEvent>) => 
    {
        const bookingId = event.currentTarget.getAttribute("data-key");
        if (bookingId !== null) setSelection(bookingId as string);
    };

    return (<ManagerPageView bind={
    {
        data: data,
        backButtonHandler: backButton,
        refreshButtonHandler: refreshButton,
        removeButtonHandler: removeButton,
        selectEventHandler: selectEvent,
        bookingId: selection
    }}/>);
}
