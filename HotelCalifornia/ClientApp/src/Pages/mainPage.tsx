import { Button, Jumbotron, Row, Col, Container } from "react-bootstrap";
import  {LinkContainer } from "react-router-bootstrap";
import { CustomColours } from "Theme/customColours";

export default function MainPage() 
{
    return (
        <>
            <Jumbotron style={{ backgroundColor: CustomColours.background.lightGray2, color: CustomColours.typography.darkGray1 }}>
                <h1>Welcome to the Hotel California!</h1>
                <p>This web site is an example of a simple web application requested by CS company.</p>
                <p>We offer:</p>
                <ul>
                    <li>4 rooms with 1 bedroom.</li>
                    <li>3 rooms: with 2 bedrooms.</li>
                    <li>3 rooms: with 3 bedrooms.</li>
                </ul>
                <LinkContainer to="/user" style={{ marginRight: "15px" }}>
                    <Button variant="primary">User page</Button>
                </LinkContainer>
                <LinkContainer to="/manager">
                    <Button variant="secondary">Manager page</Button>
                </LinkContainer>
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
