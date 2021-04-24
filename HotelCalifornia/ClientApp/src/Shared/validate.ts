import Validate from "validate.js";
import Moment from "moment";
import { IAddBookingDto } from "../Models";

const SetupDateTimeExtension = () => 
{
    Validate.extend(Validate.validators.datetime, 
    {
        parse: function(value: Moment.MomentInput) 
        {
            return +Moment.utc(value);
        },
        format: function(value: Moment.MomentInput, options: { dateOnly: any; }) 
        {
            let format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return Moment.utc(value).format(format);
        }
    });
}

const ValidateBookingForm = (props: IAddBookingDto): any =>
{
    const todayUtc = Moment().utc().format("YYYY-MM-DD")
    const tomorrowUtc = Moment().utc().add(1, "day").format("YYYY-MM-DD");
    
    let restrictedDates: string[] = [];
    restrictedDates.push(props.DateFrom);

    let constraints = 
    {
        GuestFullName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                maximum: 255,
                message: "must be at least 2 characters"
            }
        },
        GuestPhoneNumber:
        {
            presence: true,
            length: 
            {
                minimum: 9,
                maximum: 12,
                message: "must be at least 9 digits (or 12 if prefix is used)"
            }
        },
        BedroomsNumber: 
        {
            presence: true,
            numericality:
            {
                onlyInteger: true,
                greaterThan: 0,
                lessThanOrEqualTo: 3,
                notInteger: "must be an integer",
                notValid: "must be between 1 and 3"
            }
        },
        DateFrom:
        {
            presence: true,
            datetime:
            {
                dateOnly: true,
                earliest: todayUtc,
                message: "must be greater or equal than today"
            }
        },
        DateTo:
        {
            presence: true,
            datetime: 
            {
                dateOnly: true,
                earliest: tomorrowUtc,
                message: "must be greater than today"
            },
            exclusion:
            {
                within: restrictedDates,
                message: "must not be the same as starting date"
            }
        }
    };

    SetupDateTimeExtension();

    return Validate(
    {
        GuestFullName: props.GuestFullName,
        GuestPhoneNumber: props.GuestPhoneNumber,
        BedroomsNumber: props.BedroomsNumber,
        DateFrom: props.DateFrom,
        DateTo: props.DateTo
    }, 
    constraints);
}

export 
{
    ValidateBookingForm
}