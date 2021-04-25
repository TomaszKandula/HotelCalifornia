import { Button, Jumbotron, Row, Col, Container } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { IGetRoomsInfo, IRoomsInfo } from "Redux/States/getRoomsInfoState";
import { CustomColours } from "Theme/customColours";

interface IBinding 
{
    bind: IGetRoomsInfo;
}

export const MainPageView = (props: IBinding) => 
{
    return (
        <>
            <Jumbotron style={{ backgroundColor: CustomColours.background.lightGray2, color: CustomColours.typography.darkGray1 }}>
                <h1>Hotel California</h1>
                <p>...this could be Heaven or this could be Hell.</p>
                <p>...voices down the corridor I thought I heard them say.</p>
                <p>Welcome to the Hotel California!</p>
                <p>Such a lovely place, such a lovely rooms:</p>
                <ul>
                    {props.bind.roomsInfo.map((items: IRoomsInfo) => (
                        <li>{items.info}</li>
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
            </Jumbotron>
            <Container style={{ color: CustomColours.typography.darkGray1 }} fluid>
                <Row>
                    <Col>
                        <h3>Tech-Stack</h3>
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
                    </Col>
                </Row>
            </Container>
            <Container style={{ marginTop: "25px", backgroundColor: CustomColours.background.lightGray2 }} fluid>
                <Row>
                    <Col></Col>
                    <Col>
                        <div style={{ textAlign: "center", padding: "25px", color: CustomColours.typography.gray2 }}>
                            &copy; Hotel California 2021.
                        </div>
                    </Col>
                    <Col></Col>
                </Row>
            </Container>
        </>
    );
}
