import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import MainPage from "./Pages/mainPage";
import UserPage from "./Pages/userPage";
import ManagerPage from "./Pages/managerPage";

//import 'bootstrap/dist/css/bootstrap.css';

export default function App() 
{
    return (
        <Router>
            <Switch>
                <Route exact path="/"><MainPage /></Route>
                <Route exact path="/user"><UserPage /></Route>
                <Route exact path="/manager"><ManagerPage /></Route>
            </Switch>
        </Router>
    );
}
