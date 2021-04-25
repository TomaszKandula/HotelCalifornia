# Hotel California

## Why has this demo been created?

Undisclosed manager has requested this web application demo. The request was to create an application that will help small private hotels let visitors book the rooms. The mandatory tech-stack was .NET Core (WebAPI with Swagger), React and Bootstrap.

## The hotel

The hotel has ten rooms. Each room has a number from 1 to 10. Four rooms have one bedroom, three rooms with two bedrooms, and three bedrooms. Each room can be reserved for a minimum of one day.

## Provided user stories

As a user of this application, I want to provide my full name, phone, desired room configuration (number of bedrooms) and dates. If there is a room that suits my needs, I should get a confirmation and the room number. The system should keep other reservations confidential from the requestor. If the desired term is not available, then there should be a message to select another date or room configuration.

As a manager, I want to be able to review the list of bookings with all details in a grid manner so I will be able to contact people personally by phone and discuss further details. If the reservation should be cancelled, there should be an option for it on the manager's screen.

## Additional information

Authorization and authentication are not needed for this application. There should be a separate URL for the visitor and the manager. Persisting the information is not needed. Expecting the reservations should be available only while the webserver is working.

## Final tech-stack

### Front-end

1. React with TypeScript.
1. React-Bootstrap.
1. Redux.
1. Axios.
1. Validate.js.
1. Moment.js.
1. Jest. 

### Back-end

1. NET Core 5 / WebAPI.
1. CQRS pattern with no event sourcing.
1. SQL Database with EF Core.
1. FluentValidation.
1. FluentAssertions.
1. MediatR library.
1. Swagger-UI.
1. Sentry.
1. xUnit.  

## Unit tests

Unit tests are provided for backend and frontend. To run backend tests, use command `dotnet test`; for frontend test, use command `yarn test`.

## How to run?

### Backend

Clone repository and open with JetBrains Rider or Visual Studio 2019. Click **run**, web browser will be opened with Swagger UI, so the API can be also explored.

### Frontend

Make sure you have **yarn** and **node** installed. Open `ClientApp` folder in Visual Studio Code, then open terminal and type `yarn install`. Create file `.env.local` by copying file `.env`, provide with values:

```
REACT_APP_API_VER=1
REACT_APP_BACKEND=http://localhost:5000
```

Finally, run command `yarn start`. After successfull compilation application will start in a web browser.

## End note

This demo may be further extended beyond what the manager has requested.
