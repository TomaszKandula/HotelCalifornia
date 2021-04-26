import { ConvertPropsToFields, HtmlRenderLines } from "./helpers";
import { 
    BOOKING_ERROR, 
    BOOKING_SUCCESS,
    BOOKING_WARNING,
    UNEXPECTED_STATUS
} from "../Shared/constants";

// BOOKINGS

const BookingSuccess = (roomNumber: number): string =>
{
    return BOOKING_SUCCESS.replace("{ROOM_NUMBER}", roomNumber.toString());
}

const BookingWarning = (object: any): string =>
{
    return BOOKING_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

const BookingError = (error: string): string =>
{
    return BOOKING_ERROR.replace("{ERROR}", error);
}

// OTHER

const UnexpectedStatusCode = (statusCode: number): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", statusCode.toString());
}

export 
{
    BookingSuccess,
    BookingWarning,
    BookingError,
    UnexpectedStatusCode
}