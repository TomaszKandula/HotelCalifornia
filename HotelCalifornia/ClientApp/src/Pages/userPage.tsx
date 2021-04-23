import React from "react";
import { Button, Container, Form, Nav, Row } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { ExecuteRequest, promiseDefaultResult } from "../Api/requests";

const formDefaultValues = 
{
    GuestFullName: "",
    GuestPhoneNumber: "",
    BedroomsNumber: 0,
    DateFrom: "",
    DateTo: ""
}

const EndpointUrl: string = "http://localhost:5000/api/v1/Booking/AddBooking";

export default function UserPage() 
{
    const [progress, setProgress] = React.useState(false);
    const [form, setForm] = React.useState(formDefaultValues);
    const [result, setResult] = React.useState(promiseDefaultResult);

    const submitForm = React.useCallback(async (payload: any) => 
    {
        setResult(await ExecuteRequest(EndpointUrl, "POST", payload));
    }, [ ]);

    React.useEffect(() => 
    {
        if (!progress) return;
        
        if (progress && result.status === null)
        {
            submitForm(form);
            return;
        }

        setProgress(false);
        setForm(formDefaultValues);
        setResult(promiseDefaultResult);

    }, [ submitForm, form, progress, result.data, result.status ]);

    const buttonHandler = () => 
    {
        setProgress(true);
    };

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); };

    return (
        <Container style={{ "marginTop": "25px" }}>
            <LinkContainer to="/">
                <Nav.Link>Back</Nav.Link>
            </LinkContainer>
            <Row>
                <h1>Book the room!</h1>
            </Row>
            <Row>
                <Form>
                    <Form.Group controlId="formFullName">
                        <Form.Label>Full name:</Form.Label>
                        <Form.Control type="text" placeholder="Enter full name" value={form.GuestFullName} onChange={formHandler} name="GuestFullName" />
                        <Form.Text className="text-muted">Maximum 255 characters.</Form.Text>
                    </Form.Group>
                    <Form.Group controlId="formPhoneNumber">
                        <Form.Label>Phone number:</Form.Label>
                        <Form.Control type="text" placeholder="Phone number" value={form.GuestPhoneNumber} onChange={formHandler} name="GuestPhoneNumber" />
                        <Form.Text className="text-muted">Maximum 12 digits.</Form.Text>
                    </Form.Group>
                    <Form.Group controlId="formBedrooms">
                        <Form.Label>Number of bedrooms:</Form.Label>
                        <Form.Control type="number" placeholder="Bedroom(s)" value={form.BedroomsNumber} onChange={formHandler} name="BedroomsNumber" />
                    </Form.Group>
                    <Form.Group controlId="formDateFrom">
                        <Form.Label>Date from:</Form.Label>
                        <Form.Control type="date" placeholder="" value={form.DateFrom} onChange={formHandler} name="DateFrom" />
                        <Form.Text className="text-muted">This is the day you arrive.</Form.Text>
                    </Form.Group>
                    <Form.Group controlId="formDateTo">
                        <Form.Label>Date to:</Form.Label>
                        <Form.Control type="date" placeholder="" value={form.DateTo} onChange={formHandler} name="DateTo" />
                        <Form.Text className="text-muted">This is the day you leave.</Form.Text>
                    </Form.Group>
                    <Button variant="primary" type="button" style={{ "marginTop": "10px" }} onClick={buttonHandler} >
                        Book it!
                    </Button>
                </Form>
            </Row>
        </Container>
    );
}
