export interface IGetAllBookings
{
    isLoading: boolean;
    bookings: IBookings[];
}

export interface IBookings
{
    id: string;
    roomNumber: number;
    bedrooms: number;
    guestFullName: string;
    guestPhoneNumber: string;
    dateFrom: string;
    dateTo: string;
}
