import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { LinkContainer } from "react-router-bootstrap";
import { Button, Container, Form, Row, Col } from "react-bootstrap";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators } from "../Redux/Actions/addBookingAction";
import { IAddBookingDto } from "../Models";
import { BookingError, BookingSuccess, BookingWarning } from "../Shared/textWrappers";
import { IconType, OperationStatus } from "../Shared/enums";
import { ValidateBookingForm } from "../Shared/validate";
import { AlertDialog, alertModalDefault } from "../Components/alertDialog";
import Validate from "validate.js";

const formDefaultValues: IAddBookingDto = 
{
    GuestFullName: "",
    GuestPhoneNumber: "",
    BedroomsNumber: 0,
    DateFrom: "",
    DateTo: ""
}

export default function UserPage() 
{
    const [progress, setProgress] = React.useState(false);
    const [modal, setModal] = React.useState(alertModalDefault);
    const [form, setForm] = React.useState(formDefaultValues);

    const showSuccess = (text: string) => { setModal({ state: true, title: "Booking", message: text, icon: IconType.info }); };
    const showWarning = (text: string) => { setModal({ state: true, title: "Warning", message: text, icon: IconType.warning }); };
    const showError = (text: string) => { setModal({ state: true, title: "Error", message: text, icon: IconType.error }); };
        
    const addBookingState = useSelector((state: IApplicationState) => state.addBooking);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const dispatch = useDispatch();

    const addBooking = React.useCallback((payload: IAddBookingDto) => 
    { dispatch(ActionCreators.addBooking(payload)); }, [ dispatch ]);

    const addBookingClear = React.useCallback(() => 
    { dispatch(ActionCreators.addBookingClear()); }, [ dispatch ]);

    React.useEffect(() => 
    {
        if (addBookingState === undefined) return;
        if (addBookingState.isAddingBooking === OperationStatus.notStarted && progress)
        {
            addBooking(
            { 
                GuestFullName: form.GuestFullName,
                GuestPhoneNumber: form.GuestPhoneNumber,
                BedroomsNumber: form.BedroomsNumber,
                DateFrom: form.DateFrom,
                DateTo: form.DateTo
            });
            return;
        }

        if (addBookingState.isAddingBooking === OperationStatus.hasFinished 
            || addBookingState.isAddingBooking === OperationStatus.hasFailed)
        {
            setProgress(false);
            setForm(formDefaultValues);

            if (addBookingState.hasAddedBooking 
                && addBookingState.isAddingBooking === OperationStatus.hasFinished)
            {
                showSuccess(BookingSuccess());
                return;
            }

            showError(BookingError(raiseErrorState.attachedErrorObject));
        }       
    }, 
    [ addBooking, addBookingClear, addBookingState, form, progress ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); };

    const modalHandler = () => 
    { 
        addBookingClear();
        setModal({ ...modal, state: false }); 
    };

    const buttonHandler = () => 
    {
        let results = ValidateBookingForm(form);
        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(BookingWarning(results));
    };

    return (
        <Container>
            <AlertDialog state={modal.state} handle={modalHandler} title={modal.title} message={modal.message} icon={modal.icon} />
            <Row>
                <Col></Col>
                <Col xs={6}>
                    <div style={{ marginTop: "30px", marginBottom: "25px" }}>
                        <h1>Book the room!</h1>
                    </div>
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
                            <Form.Text className="text-muted">Tell us how many beds do you need.</Form.Text>
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
                    </Form>
                    <div style={{ marginTop: "40px" }}>
                        <Button variant="primary" type="button" onClick={buttonHandler} size="lg" block >
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
