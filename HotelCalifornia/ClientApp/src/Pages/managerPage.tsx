import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "Redux/Actions/getAllBookingsAction";
import { IApplicationState } from "Redux/applicationState";
import { ManagerPageView } from "./managerPageView";

export default function ManagerPage() 
{
    const data = useSelector((state: IApplicationState) => state.getAllBookings);
    const dispatch = useDispatch();

    const fetchData = React.useCallback(() => 
    { dispatch(ActionCreators.requestBooking()); }, [ dispatch ]);
    
    React.useEffect(() => { fetchData() }, [ fetchData ]);

    return (<ManagerPageView bind={data} />);
}
