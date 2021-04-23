const API_VER = process.env.REACT_APP_API_VER;

export const APP_BACKEND = process.env.REACT_APP_BACKEND;

export const API_QUERY_GET_BOOKINGS = `${APP_BACKEND}/api/v${API_VER}/booking/GetAllBookings/`;
export const API_COMMAND_ADD_BOOKING = `${APP_BACKEND}/api/v${API_VER}/booking/AddBooking`;
export const API_COMMAND_REMOVE_BOOKING = `${APP_BACKEND}/api/v${API_VER}/booking/RemoveBooking`;

export const NO_ERRORS = "NO_ERRORS";
