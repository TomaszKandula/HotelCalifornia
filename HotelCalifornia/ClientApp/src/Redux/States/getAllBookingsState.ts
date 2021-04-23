export interface IGetAllBookings
{
    isLoading: boolean;
    bookings: IBookings[];
}

export interface IBookings
{
    Id: string;
    RoomNumber: number;
    Bedrooms: number;
    GuestFullName: string;
    GuestPhoneNumber: string;
    DateFrom: string;
    DateTo: string;
}
