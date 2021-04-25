const API_VER = process.env.REACT_APP_API_VER;

export const APP_BACKEND = process.env.REACT_APP_BACKEND;

export const API_QUERY_GET_BOOKINGS = `${APP_BACKEND}/api/v${API_VER}/booking/GetAllBookings/`;
export const API_QUERY_GET_ROOMS = `${APP_BACKEND}/api/v${API_VER}/room/GetRoomsInfo/`;
export const API_COMMAND_ADD_BOOKING = `${APP_BACKEND}/api/v${API_VER}/booking/AddBooking`;
export const API_COMMAND_REMOVE_BOOKING = `${APP_BACKEND}/api/v${API_VER}/booking/RemoveBooking`;

export const BOOKING_SUCCESS = "<p>Congratulations!</p><p>Your room has been booked!</p>";
export const BOOKING_WARNING = "<span>We cannot book your room, following warning(s) received:</span><ul>{LIST}</ul><span>Please make sure the fields are valid.</span>";
export const BOOKING_ERROR = "<p>Upss!</p><p>We cannot book your room.</p><p>{ERROR}.</p>";

export const UNEXPECTED_STATUS = "Received unexpected status code: {STATUS_CODE}. Please contact IT Support";
export const UNEXPECTED_ERROR = "Unexpected error occured";
export const VALIDATION_ERRORS = "Validation errors have been found";

export const RECEIVED_ERROR_MESSAGE = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS = "NO_ERRORS";

export const NOT_APPLICABLE = "n/a";
export const REDIRECT_TO_MAIN = "/";
