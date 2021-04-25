import { Container, Table, Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { IGetAllBookings, IBookings } from "../Redux/States/getAllBookingsState";
import Moment from "moment";

interface IBinding 
{
    bookings: IGetAllBookings;
}

export const ManagerPageView = (props: IBinding) => 
{
    return(
        <Container style={{ marginTop: "30px", marginBottom: "25px" }}>
            <div style={{ marginTop: "15px", marginBottom: "25px" }}>
                <h1>Active bookings</h1>
            </div>
            <div style={{ marginBottom: "25px" }}>
                <LinkContainer to="/" style={{ marginRight: "15px" }}><Button variant="secondary" type="button">Back</Button></LinkContainer>
                <Button variant="primary" type="button" >Refresh</Button>
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
                    {props.bookings.bookings.map((item: IBookings) => ( 
                        <tr key={item.id}>
                            <td>{item.id.substring(0,8)}</td>
                            <td>{item.guestFullName}</td>
                            <td>{item.guestPhoneNumber}</td>
                            <td>{item.roomNumber}</td>
                            <td>{item.bedrooms}</td>
                            <td>{Moment(item.dateFrom).format("YYYY-MM-DD")}</td>
                            <td>{Moment(item.dateTo).format("YYYY-MM-DD")}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
} 
