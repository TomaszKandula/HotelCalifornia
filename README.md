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
1. SeriLog.
1. xUnit.  

## Project structure

_HotelCalifornia_

| Folder | Description |
|--------|-------------|
| ClientApp | Frontend in React^ |
| Configuration | Application dependencies |
| Controllers | WebAPI |
| Middleware | Custom middleware |

^Unit tests are provided; use command `yarn test` to run all tests.

_Backend_

| Folder | Description |
|--------|-------------|
| HotelCalifornia.Backend.Core | Reusable core elements |
| HotelCalifornia.Backend.Cqrs | Handlers, mappers and related services |
| HotelCalifornia.Backend.Database | Database context |
| HotelCalifornia.Backend.Domain | Domain entities |
| HotelCalifornia.Backend.Shared | Shared models and resources |

_Tests_

| Folder | Description |
|--------|-------------|
| HotelCalifornia.Tests.TestData | Test helpers |
| HotelCalifornia.Tests.UnitTests | Handlers and validators tests |

To run backend tests, use command `dotnet test`.

## CQRS

The project uses a CQRS architectural pattern with no event sourcing (changes to the application state are **not** stored as a sequence of events). I used the MediatR library (mediator pattern) with the handler template.

The file `TemplateHandler.cs` presented below allow easy registration (mapping the handlers).

```csharp
public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected TemplateHandler() { }

    public abstract Task<TResult> Handle(TRequest ARequest, CancellationToken ACancellationToken);
}
```

To configure it, in `Dependencies.cs` (registered at startup), we invoke:

```csharp
private static void SetupMediatR(IServiceCollection AServices) 
{
    AServices.AddMediatR(AOption => AOption.AsScoped(), 
        typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

    AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
}
```

The two additional lines register both `LoggingBehaviour` and `FluentValidationBehavior` as scoped services. Thus we not only log event before and after handler execution, but also we perform validation of payload before executing the handler.

`LoggingBehaviour.cs`:

```csharp
public async Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
{
    FLogger.LogInfo($"Begin: Handle {typeof(TRequest).Name}");
    var LResponse = await ANext();
    FLogger.LogInfo($"Finish: Handle {typeof(TResponse).Name}");
    return LResponse;
}
```

Logging is part of the middleware pipeline, and as said, we log info before and after handler execution.

`FluentValidationBehavior.cs`:

```csharp
public Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
{
    if (FValidator == null) return ANext();

    var LValidationContext = new ValidationContext<TRequest>(ARequest);
    var LValidationResults = FValidator.Validate(LValidationContext);

    if (!LValidationResults.IsValid)
        throw new ValidationException(LValidationResults);

    return ANext();
}
```

Validator is registered within the middleware pipeline, and if it exists (not null), then we execute it and raise an exception if invalid, otherwise we proceed. Note: `ValidationException.cs` inherits from `BusinessException.cs` which inherits form `System.Exception`.

Such setup allow to have very thin controllers, example endpoint:

```csharp
[HttpGet]
public async Task<IEnumerable<GetRoomsInfoQueryResult>> GetRoomsInfo()
    => await FMediator.Send(new GetRoomsInfoQuery());
```

When we call `GetRoomsInfo` endpoint, it sends `GetRoomsInfoQuery` request with given parameters. The appropriate handler is `GetRoomsInfoQueryHandler`:

```csharp
public class GetRoomsInfoQueryHandler : TemplateHandler<GetRoomsInfoQuery, IEnumerable<GetRoomsInfoQueryResult>>
{
    private const string PLURAL_SUFFIX = "s";
    
    private readonly DatabaseContext FDatabaseContext;

    public GetRoomsInfoQueryHandler(DatabaseContext ADatabaseContext)
        => FDatabaseContext = ADatabaseContext;

    public override async Task<IEnumerable<GetRoomsInfoQueryResult>> Handle(GetRoomsInfoQuery ARequest, CancellationToken ACancellationToken)
    {
        var LQueryResults =
            from LRooms in FDatabaseContext.Rooms
            group LRooms by LRooms.Bedrooms
            into LGrouping
            select new QueryRoomsInfoDto
            {
                Bedrooms = LGrouping.Key,
                TotalRooms = LGrouping.Select(ARooms => ARooms.Bedrooms).Count()
            };

        return await Task.FromResult(GetRoomsInfo(LQueryResults));
    }

    private static IEnumerable<GetRoomsInfoQueryResult> GetRoomsInfo(IEnumerable<QueryRoomsInfoDto> AQueryResults)
    {
        foreach (var LQueryResult in AQueryResults)
        {
            var LBedroomSuffix = string.Empty;
            var LRoomSuffix = string.Empty;
                
            if (LQueryResult.Bedrooms > 1)
                LBedroomSuffix = PLURAL_SUFFIX;
               
            if (LQueryResult.TotalRooms > 1)
                LRoomSuffix = PLURAL_SUFFIX;

            yield return new GetRoomsInfoQueryResult
            {
                Id = Guid.NewGuid(),
                Info = $"{LQueryResult.TotalRooms} room{LRoomSuffix} with {LQueryResult.Bedrooms} bedroom{LBedroomSuffix}."
            };
        }            
    }
}
```

## How to run?

### Backend

Clone repository and open with JetBrains Rider or Visual Studio 2019. 

Copy below code from `appsettings.Development.json` to **user secrets**: and replace `set_env` values:

```
"ConnectionStrings":
{
    "DbConnect": "set_env"
},
"AppUrls":
{
    "DevelopmentOrigin": "set_env",
    "DeploymentOrigin": "set_env"
}
```

### Origins

Use `http://localhost:3000` or any other used by the frontend.

### Development environment:

If `set_env` remains unchanged, the application will use SQL database in-memory. However, suppose one is willing to use a local/remote SQL database. In that case, they should replace `set_env` with a valid connection string (note: application always uses in-memory database when the connection string is invalid). Application seeds test when the existing tables are not populated, and migration is performed only on the local/remote SQL database.

### Manual migration

Go to Package Manager Console (PMC) to execute following command:

`Update-Database -StartupProject HotelCalifronia -Project HotelCalifronia.Backend.Database -Context DatabaseContext`

EF Core will create all the necessary tables. More on migrations here: [HotelCalifornia.Backend.Database](https://github.com/TomaszKandula/HotelCalifornia/tree/master/Backend/HotelCalifornia.Backend.Database).

### Running the backend

If all has been setup, then click **run**, web browser will be opened with Swagger UI, so the API can be also explored.

### Frontend

Make sure you have **yarn** and **node** installed. Open `ClientApp` folder in Visual Studio Code, then open terminal and type `yarn install`. Create file `.env.local` by copying file `.env`, provide with values:

```
REACT_APP_API_VER=1
REACT_APP_BACKEND=http://localhost:5000
```

Finally, run command `yarn start`. After successfull compilation application will start in a web browser.

## End note

This demo may be further extended beyond what the manager has requested.
