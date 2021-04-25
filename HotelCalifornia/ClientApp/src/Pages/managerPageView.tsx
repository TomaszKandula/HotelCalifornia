import { Container, Table, Button } from "react-bootstrap";
import { IGetAllBookings, IBookings } from "../Redux/States/getAllBookingsState";
import Moment from "moment";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    data: IGetAllBookings;
    backButtonHandler: any;
    refreshButtonHandler: any;
    removeButtonHandler: any;
    selectEventHandler: any,
    bookingId: string;
}

export const ManagerPageView = (props: IBinding) => 
{
    return(
        <Container style={{ marginTop: "30px", marginBottom: "25px" }}>
            <div style={{ marginTop: "15px", marginBottom: "25px" }}>
                <h1>Active bookings</h1>
            </div>
            <div style={{ marginBottom: "25px" }}>
                <Button variant="secondary" type="button" style={{ marginRight: "15px" }} onClick={props.bind?.backButtonHandler} >Back</Button>
                <Button variant="primary" type="button" style={{ marginRight: "15px" }} onClick={props.bind?.refreshButtonHandler}>Refresh</Button>
                <Button variant="danger" type="button" onClick={props.bind.removeButtonHandler}>Cancel booking</Button>
            </div>
            <div style={{ marginBottom: "25px" }}>
                Selection: {props.bind?.bookingId}
            </div>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Phone</th>
                        <th>Room</th>
                        <th>Bedrooms</th>
                        <th>Arrival</th>
                        <th>Departure</th>
                    </tr>
                </thead>
                <tbody>
                    {props.bind?.data.bookings.map((item: IBookings, index: number) => ( 
                        <tr key={index} data-key={item?.id} onClick={props.bind?.selectEventHandler}>
                            <td>{item?.id.substring(0,8)}</td>
                            <td>{item?.guestFullName}</td>
                            <td>{item?.guestPhoneNumber}</td>
                            <td>{item?.roomNumber}</td>
                            <td>{item?.bedrooms}</td>
                            <td>{Moment(item?.dateFrom).format("YYYY-MM-DD")}</td>
                            <td>{Moment(item?.dateTo).format("YYYY-MM-DD")}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
} 
