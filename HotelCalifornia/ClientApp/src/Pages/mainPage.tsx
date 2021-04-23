import { Button, Jumbotron, Nav } from "react-bootstrap";
import {LinkContainer} from "react-router-bootstrap";

export default function MainPage() 
{
    return (
        <Jumbotron>
            <h1>Welcome to the Hotel California!</h1>
            <p>
                This web site is an example of a web application requested by client.
            </p>
            <p>
                To book the room, please click "User page".
            </p>
            <p>
                To see the bookings, please click "Manager page".
            </p>
            <p>
                We offer:
            </p>
            <ul>
                <li>
                    4 rooms with 1 bedroom.
                </li>
                <li>
                    3 rooms: with 2 bedrooms.
                </li>
                <li>
                    3 rooms: with 3 bedrooms.
                </li>
            </ul>
            <p>
                Enjoy!
            </p>
            <p>
                <LinkContainer to="/user">
                    <Nav.Link>
                        <Button variant="primary">User page</Button>
                    </Nav.Link>
                </LinkContainer>
            </p>
            <p>
                <LinkContainer to="/manager">
                    <Nav.Link>
                    <Button variant="secondary">Manager page</Button>
                    </Nav.Link>
                </LinkContainer>
            </p>
        </Jumbotron>    
    );
}
