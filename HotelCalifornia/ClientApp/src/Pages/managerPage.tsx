import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ActionCreators as ActionCreatorsRequest } from "../Redux/Actions/getAllBookingsAction";
import { ActionCreators as ActionCreatorsRemove } from "../Redux/Actions/removeBookingAction";
import { IApplicationState } from "../Redux/applicationState";
import { ManagerPageView } from "./managerPageView";

export default function ManagerPage() 
{
    const [selection, setSelection] = React.useState("n/a");
    const [reload, setReload] = React.useState(false);
    const [remove, setRemove] = React.useState(false);

    const data = useSelector((state: IApplicationState) => state.getAllBookings);
    const history = useHistory();
    const dispatch = useDispatch();

    const fetchData = React.useCallback(() => 
    { dispatch(ActionCreatorsRequest.requestBooking()); }, [ dispatch ]);
 
    const deleteBooking = React.useCallback((bookingId: string) => 
    { dispatch(ActionCreatorsRemove.removeBooking({ BookingId: bookingId })); }, [ dispatch ]);

    React.useEffect(() => 
    { 
        fetchData(); 
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
        if (remove && selection !== "n/a") deleteBooking(selection);
        setRemove(false);
    }, 
    [ deleteBooking, remove ]);
    
    const backButton = () => 
    {
        dispatch(ActionCreatorsRequest.resetSelection());
        history.push("/");
    };

    const refreshButton = () => 
    {
        setReload(true);
    };

    const removeButton = () => 
    {
        setRemove(true);
    };

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
