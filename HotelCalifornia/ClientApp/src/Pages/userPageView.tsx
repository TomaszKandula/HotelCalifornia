import { LinkContainer } from "react-router-bootstrap";
import { Button, Container, Form, Row, Col, Card } from "react-bootstrap";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    formHandler: any;
    buttonHandler: any;
    guestFullName: string;
    guestPhoneNumber: string;
    bedroomsNumber: number;
    dateFrom: string;
    dateTo: string;
}

export const UserPageView = (props: IBinding) => 
{
    return (
        <Container>
            <Row style={{ marginTop: "30px", marginBottom: "25px" }}>
                <Col></Col>
                <Col xs={6}>
                    <Card body>
                    <div style={{ marginTop: "10px", marginBottom: "20px" }}>
                            <h1>Book the room!</h1>
                        </div>
                        <Form>
                            <Form.Group controlId="formFullName">
                                <Form.Label>Full name:</Form.Label>
                                <Form.Control type="text" placeholder="Enter full name" value={props.bind?.guestFullName} onChange={props.bind?.formHandler} name="GuestFullName" />
                                <Form.Text className="text-muted">Maximum 255 characters.</Form.Text>
                            </Form.Group>
                            <Form.Group controlId="formPhoneNumber">
                                <Form.Label>Phone number:</Form.Label>
                                <Form.Control type="text" placeholder="Phone number" value={props.bind?.guestPhoneNumber} onChange={props.bind?.formHandler} name="GuestPhoneNumber" />
                                <Form.Text className="text-muted">Maximum 12 digits.</Form.Text>
                            </Form.Group>
                            <Row>
                                <Col xs={6}>
                                    <Form.Group controlId="formBedrooms">
                                        <Form.Label>Number of bedrooms:</Form.Label>
                                        <Form.Control type="number" placeholder="Bedroom(s)" value={props.bind?.bedroomsNumber} onChange={props.bind?.formHandler} name="BedroomsNumber" />
                                        <Form.Text className="text-muted">Tell us how many beds do you need.</Form.Text>
                                    </Form.Group>
                                    <Form.Group controlId="formDateFrom">
                                        <Form.Label>Date from:</Form.Label>
                                        <Form.Control type="date" placeholder="" value={props.bind?.dateFrom} onChange={props.bind?.formHandler} name="DateFrom" />
                                        <Form.Text className="text-muted">This is the day you arrive.</Form.Text>
                                    </Form.Group>
                                    <Form.Group controlId="formDateTo">
                                        <Form.Label>Date to:</Form.Label>
                                        <Form.Control type="date" placeholder="" value={props.bind?.dateTo} onChange={props.bind?.formHandler} name="DateTo" />
                                        <Form.Text className="text-muted">This is the day you leave.</Form.Text>
                                    </Form.Group>
                                </Col>
                            </Row>
                        </Form>
                        <div style={{ marginTop: "20px" }}>
                            <Button variant="primary" type="button" onClick={props.bind?.buttonHandler} size="lg" block >
                                Book
                            </Button>
                            <LinkContainer to="/">
                                <Button variant="secondary" type="button" size="lg" block >
                                    Back
                                </Button>
                            </LinkContainer>
                        </div>
                    </Card>
                </Col>
                <Col></Col>
            </Row>
        </Container>
    );
}