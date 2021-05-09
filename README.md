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
| ClientApp | Frontend in React |
| Configuration | Application dependencies |
| Controllers | WebAPI |
| Middleware | Custom middleware |

In the current project version, the static bundles is hosted alongside the ASP.NET Core server-side application. This is the most straightforward approach, which works well in many situations. During the build process, the bundles are generated and copied to a preconfigured folder inside the ASP.NET Core application.

Unit tests are provided; use command `yarn test` to run all tests for the frontend.

_Backend_

| Folder | Description |
|--------|-------------|
| Backend.Core | Reusable core elements |
| Backend.Cqrs | Handlers, mappers and related services |
| Backend.Database | Database context |
| Backend.Domain | Domain entities |
| Backend.Shared | Shared models and resources |

_Tests_

| Folder | Description |
|--------|-------------|
| IntegrationTests | Http client tests |
| UnitTests | Handlers and validators tests |

To run backend tests, use command `dotnet test`.

## Testing

### Unit Tests setup

Unit tests use SQLite in-memory database (a lightweight database that supports RDBMS). Each test uses a separate database instance, and therefore tables must be populated before a test can be run. Database instances are provided via the factory:

```csharp
internal class DatabaseContextFactory
{
    private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions =
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite("Data Source=InMemoryDatabase;Mode=Memory");

    public DatabaseContext CreateDatabaseContext()
    {
        var LDatabaseContext = new DatabaseContext(FDatabaseOptions.Options);
        LDatabaseContext.Database.OpenConnection();
        LDatabaseContext.Database.EnsureCreated();
        return LDatabaseContext;
    }
}
```

Each test can easily access `CreateDatabaseContext()` method via `GetTestDatabaseContext()` as long as test class inherits from `TestBase` class:

```csharp
public class TestBase
{
    private readonly DatabaseContextFactory FDatabaseContextFactory;

    protected TestBase() 
    {
        var LServices = new ServiceCollection();

        LServices.AddSingleton<DatabaseContextFactory>();
        LServices.AddScoped(AContext =>
        {
            var LFactory = AContext.GetService<DatabaseContextFactory>();
            return LFactory?.CreateDatabaseContext();
        });

        var LServiceScope = LServices.BuildServiceProvider(true).CreateScope();
        var LServiceProvider = LServiceScope.ServiceProvider;
        FDatabaseContextFactory = LServiceProvider.GetService<DatabaseContextFactory>();
    }

    protected DatabaseContext GetTestDatabaseContext()
        =>  FDatabaseContextFactory.CreateDatabaseContext();
}
```

### Integration Tests setup

Integration test uses SQL Server database either local or remote, accordingly to a given connection string. Each test class uses `WebApplicationFactory`:

```csharp
public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var LBuilder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(AConfig =>
            {
                var LStartupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                var LTestConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(LStartupAssembly)
                    .AddEnvironmentVariables()
                    .Build();
              
                AConfig.AddConfiguration(LTestConfig);
            })
            .UseStartup<TTestStartup>()
            .UseTestServer();
            
        return LBuilder;
    }
}
```

I use `user secrets` with a connection string for local development, pointing to an instance of SQL Express that runs in Docker. However, if the test project would run in CI/CD pipeline, then we use connection string defined in `appsettings.Staging.json` for a remote test database.

Class `CustomWebApplicationFactory` requires the `Startup` class to configure necessary services. Thus test project has its own `TestStartup.cs` that inherits from the main project `Startup.cs`. We register only necessary services.

Note: before integration tests can run, test database must be up.

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

## Exception Handler

After adding custom exception handler to the middleware pipeline:

```csharp
AApplication.UseExceptionHandler(ExceptionHandler.Handle);
```

It will catch exceptions and sets HTTP status: bad request (400) or internal server error (500). Thus, if we throw an error (business or validation) manually in the handler, the response is appropriately set up.

```csharp
public static class ExceptionHandler
{
    public static void Handle(IApplicationBuilder AApplication)
    {
        AApplication.Run(async AHttpContext => 
        {
            var LExceptionHandlerPathFeature = AHttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var LErrorException = LExceptionHandlerPathFeature.Error;
            AHttpContext.Response.ContentType = "application/json";

            string LResult;
            switch (LErrorException)
            {
                case ValidationException LException:
                {
                    var LAppError = new ApplicationError(LException.ErrorCode, LException.Message, LException.ValidationResult);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }

                case BusinessException LException:
                {
                    var LAppError = new ApplicationError(LException.ErrorCode, LException.Message);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }

                default:
                {
                    var LAppError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                }
            }

            CorsHeaders.Ensure(AHttpContext);
            await AHttpContext.Response.WriteAsync(LResult);
        });
    }
}
```

Please note that handlers usually contains manual business exceptions while having validation exceptions very rarely as they are typically raised by the `FluentValidation` before handler is invoked, an example being:

```csharp
public override async Task<AddBookingCommandResult> Handle(AddBookingCommand ARequest, CancellationToken ACancellationToken)
{
    var LRoomsWithBedrooms = await FDatabaseContext.Rooms
        .Where(ARooms => ARooms.Bedrooms == ARequest.BedroomsNumber)
        .Select(ARoom => ARoom.Id)
        .ToListAsync(ACancellationToken);

    if (!LRoomsWithBedrooms.Any())
        throw new BusinessException(
            nameof(ErrorCodes.REQUESTED_BEDROOMS_UNAVAILABLE),
            ErrorCodes.REQUESTED_BEDROOMS_UNAVAILABLE);
            
    var LRoomsTaken = await FDatabaseContext.Bookings
        .Where(ABookings => LRoomsWithBedrooms.Contains(ABookings.RoomId) 
            && ABookings.DateFrom == ARequest.DateFrom 
            && ABookings.DateTo == ARequest.DateTo)
        .Select(ABookings => ABookings.RoomId)
        .ToListAsync(ACancellationToken);

    var LFreeSlots = LRoomsWithBedrooms.Except(LRoomsTaken).ToList();
            
    if (!LFreeSlots.Any())
        throw new BusinessException(nameof(
            ErrorCodes.NO_AVAILABLE_ROOMS), 
            ErrorCodes.NO_AVAILABLE_ROOMS);

    var LNewBooking = new Bookings
    {
        RoomId = LFreeSlots.First(),
        GuestFullName = ARequest.GuestFullName,
        GuestPhoneNumber = ARequest.GuestPhoneNumber,
        DateFrom = ARequest.DateFrom,
        DateTo = ARequest.DateTo
    };

    FDatabaseContext.Bookings.Add(LNewBooking);
    await FDatabaseContext.SaveChangesAsync(ACancellationToken);

    var LRoomNumber = await FDatabaseContext.Rooms
        .Where(ARooms => ARooms.Id == LNewBooking.RoomId)
        .Select(ARooms => ARooms.RoomNumber)
        .SingleOrDefaultAsync(cancellationToken: ACancellationToken);
            
    return new AddBookingCommandResult
    {
        Id = LNewBooking.Id,
        RoomNumber = LRoomNumber
    };
}
```

These business exceptions (`REQUESTED_BEDROOMS_UNAVAILABLE` and `NO_AVAILABLE_ROOMS`) shall never be validation errors (invoked by `FluentValidation`). Furthermore, it is unlikely that we would want to perform database requests during validation. The validator is responsible for ensuring that input data is valid (not for checking available rooms etc.).

## How to run?

### Backend

Clone repository and open with JetBrains Rider or Visual Studio 2019. 

Copy below code from `appsettings.Development.json` to **user secrets**: and replace `set_env` values:

```
"ConnectionStrings":
{
    "DbConnect": "set_env",
    "DbConnectTest": "set_env"
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

Replace `set_env` with connection strings of choice. Please note that `DbConnect` points to a main database (local development / production), and `DbConnectTest` points to a test database for integration tests only. Application migarte and seed tests data when run in development mode, however, for integration tests, test database must be already up. 

### Manual migration

Go to Package Manager Console (PMC) to execute following command:

`Update-Database -StartupProject HotelCalifronia -Project HotelCalifronia.Backend.Database -Context DatabaseContext`

EF Core will create all the necessary tables and seed demo data. More on migrations here: [HotelCalifornia.Backend.Database](https://github.com/TomaszKandula/HotelCalifornia/tree/master/Backend/HotelCalifornia.Backend.Database).

### Running the backend

If all has been setup, then click **run**, web browser will be opened with Swagger UI, so the API can be also explored.

### Frontend

Make sure you have **yarn** and **node** installed. Open `ClientApp` folder in Visual Studio Code, then open terminal and type `yarn install`. Create file `.env.local` by copying file `.env`, provide with values:

```
REACT_APP_API_VER=1
REACT_APP_BACKEND=http://localhost:5000
```

Finally, run command `yarn start`. After successful compilation application will start in a web browser.

## End note

This demo may be further extended beyond what the manager has requested.
