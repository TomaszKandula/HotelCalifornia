import { LinkContainer } from "react-router-bootstrap";
import { Button, Container, Form, Row, Col } from "react-bootstrap";
import { AlertDialog } from "../Components/alertDialog";
import { IconType } from "Shared/enums";

interface IUserPageView 
{
    state: boolean;
    handle: any;
    title: string;
    message: string;
    icon: IconType;
    formHandler: any;
    buttonHandler: any;
    guestFullName: string;
    guestPhoneNumber: string;
    bedroomsNumber: number;
    dateFrom: string;
    dateTo: string;
}

export const UserPageView = (props: IUserPageView) => 
{
    return (
        <Container>
            <AlertDialog state={props.state} handle={props.handle} title={props.title} message={props.message} icon={props.icon} />
            <Row>
                <Col></Col>
                <Col xs={6}>
                    <div style={{ marginTop: "30px", marginBottom: "25px" }}>
                        <h1>Book the room!</h1>
                    </div>
                    <Form>
                        <Form.Group controlId="formFullName">
                            <Form.Label>Full name:</Form.Label>
                            <Form.Control type="text" placeholder="Enter full name" value={props.guestFullName} onChange={props.formHandler} name="GuestFullName" />
                            <Form.Text className="text-muted">Maximum 255 characters.</Form.Text>
                        </Form.Group>
                        <Form.Group controlId="formPhoneNumber">
                            <Form.Label>Phone number:</Form.Label>
                            <Form.Control type="text" placeholder="Phone number" value={props.guestPhoneNumber} onChange={props.formHandler} name="GuestPhoneNumber" />
                            <Form.Text className="text-muted">Maximum 12 digits.</Form.Text>
                        </Form.Group>
                       <Form.Group controlId="formBedrooms">
                            <Form.Label>Number of bedrooms:</Form.Label>
                            <Form.Control type="number" placeholder="Bedroom(s)" value={props.bedroomsNumber} onChange={props.formHandler} name="BedroomsNumber" />
                            <Form.Text className="text-muted">Tell us how many beds do you need.</Form.Text>
                        </Form.Group>
                        <Form.Group controlId="formDateFrom">
                            <Form.Label>Date from:</Form.Label>
                            <Form.Control type="date" placeholder="" value={props.dateFrom} onChange={props.formHandler} name="DateFrom" />
                            <Form.Text className="text-muted">This is the day you arrive.</Form.Text>
                        </Form.Group>
                        <Form.Group controlId="formDateTo">
                            <Form.Label>Date to:</Form.Label>
                            <Form.Control type="date" placeholder="" value={props.dateTo} onChange={props.formHandler} name="DateTo" />
                            <Form.Text className="text-muted">This is the day you leave.</Form.Text>
                        </Form.Group>
                    </Form>
                    <div style={{ marginTop: "40px" }}>
                        <Button variant="primary" type="button" onClick={props.buttonHandler} size="lg" block >
                            Book
                        </Button>
                        <LinkContainer to="/">
                            <Button variant="secondary" type="button" size="lg" block >
                                Back
                            </Button>
                        </LinkContainer>
                    </div>
                </Col>
                <Col></Col>
            </Row>
        </Container>
    );
}