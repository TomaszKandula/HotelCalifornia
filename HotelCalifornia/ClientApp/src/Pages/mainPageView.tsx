import { Button, Jumbotron, Col, Container } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { IGetRoomsInfo, IRoomsInfo } from "../Redux/States/getRoomsInfoState";
import { CustomColours } from "../Theme/customColours";

interface IBinding 
{
    bind: IGetRoomsInfo;
}

export const MainPageView = (props: IBinding) => 
{
    return (
        <>
            <Jumbotron style={{ backgroundColor: CustomColours.background.lightGray2, color: CustomColours.typography.darkGray1 }}>
                <Container>
                    <h1>Hotel California</h1>
                    <p>...this could be Heaven or this could be Hell.</p>
                    <p>...voices down the corridor I thought I heard them say.</p>
                    <p>Welcome to the Hotel California!</p>
                    <p>Such a lovely place, such a lovely rooms:</p>
                    <ul>
                        {props.bind?.roomsInfo.map((items: IRoomsInfo) => (
                            <li key={items.id}>{items.info}</li>
                        ))}
                    </ul>
                    <div style={{ marginTop: "35px" }}>
                        <LinkContainer to="/user" style={{ marginRight: "15px" }}>
                            <Button variant="primary">User page</Button>
                        </LinkContainer>
                        <LinkContainer to="/manager">
                            <Button variant="secondary">Manager page</Button>
                        </LinkContainer>
                    </div>
                </Container>
            </Jumbotron>
            <Jumbotron style={{ backgroundColor: CustomColours.background.white, color: CustomColours.typography.darkGray1 }}>
                <Container style={{ marginTop: "-60px", marginBottom: "-40px" }}>
                    <div style={{ marginTop: "0px" }}>
                        <h3>Why has this demo been created?</h3>
                        <hr />
                        <p>Credit Suisse manager has requested this web application demo. The request was to create an application that will help small private hotels let visitors book the rooms. The mandatory tech-stack was .NET Core (WebAPI with Swagger), React and Bootstrap.</p>
                    </div>
                    <div style={{ marginTop: "30px" }}>
                        <h3>The hotel</h3>
                        <hr />
                        <p>The hotel has ten rooms. Each room has a number from 1 to 10. 4 rooms have one bedroom, three rooms having two bedrooms, and three rooms with three bedrooms. Each room can be reserved for a minimum of one day.</p>
                    </div>
                    <div style={{ marginTop: "30px" }}>
                        <h3>Provided user stories</h3>
                        <hr />
                        <p>As a user of this application, I want to provide my full name, phone, desired room configuration (number of bedrooms) and dates. If there is a room that suits my needs, I should get a confirmation and the room number. The system should keep other reservations confidential from the requestor. If the desired term is not available, then there should be a message to select another date or room configuration.</p>
                        <p>As a manager, I want to be able to review the list of bookings with all details in a grid manner so I will be able to contact people personally by phone and discuss further details. If the reservation should be cancelled, there should be an option for it on the manager's screen.</p>
                    </div>
                    <div style={{ marginTop: "30px" }}>
                        <h3>Additional information</h3>
                        <hr />
                        <p>Authorization and authentication are not needed for this application. There should be a separate URL for the visitor and the manager.</p>
                        <p>Persisting the information is not needed. Expecting the reservations should be available only while the webserver is working.</p>
                    </div>
                    <div style={{ marginTop: "30px" }}>
                        <h3>Final tech-stack</h3>
                        <hr />
                        <p>Front-end:</p>
                        <ul>
                            <li>React with TypeScript.</li>
                            <li>React-Bootstrap.</li>
                            <li>Axios.</li>
                            <li>Validate.js</li>
                            <li>Moment.js</li>
                            <li>JEST</li>
                        </ul>
                        <p>Back-end:</p>
                        <ul>
                            <li>NET Core 5 / WebAPI.</li>
                            <li>CQRS pattern with no event sourcing.</li>
                            <li>SQL Database with EF Core.</li>
                            <li>FluentValidation.</li>
                            <li>MediatR library.</li>
                            <li>Swagger-UI.</li>
                        </ul>
                    </div>
                    <div style={{ marginTop: "30px" }}>
                        <h3>Unit tests</h3>
                        <hr />
                        <p>Unit tests are provided for backend and frontend. To run backend tests, use command <b>dotnet test</b>; for frontend test, use command <b>yarn test</b>.</p>
                    </div>
                </Container>
            </Jumbotron>
            <Container style={{ marginTop: "25px", backgroundColor: CustomColours.background.lightGray2 }} fluid>
                <Col></Col>
                <Col>
                    <div style={{ textAlign: "center", padding: "25px", color: CustomColours.typography.gray2 }}>
                        &copy; Hotel California 2021.
                    </div>
                </Col>
                <Col></Col>
            </Container>
        </>
    );
}
