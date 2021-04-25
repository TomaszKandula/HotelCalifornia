import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators } from "../Redux/Actions/getRoomsInfoAction";
import { IApplicationState } from "../Redux/applicationState";
import { MainPageView } from "./mainPageView";

export default function MainPage() 
{
    const data = useSelector((state: IApplicationState) => state.getRoomsInfo);
    const dispatch = useDispatch();

    const fetchData = React.useCallback(() => 
    { dispatch(ActionCreators.requestRoomsInfo()); }, [ dispatch ]);
    
    React.useEffect(() => { fetchData() }, [ fetchData ]);
    
    return (<MainPageView bind={data} />);
}
