# RoutingRecords

RoutingRecords is a small set of tools that help Asp.Net Core developers to program cool and simple APIs in .Net 5. It relies on `record` type objects to define inline endpoints:

```csharp
public record Hello()
    : Get("/", (req, res) =>
        res.SendAsync("Welcome to RoutingRecords"));
```

Main features:

- **Minimalistic**: You can define an endpoint in a single line of code.
- **Fast**: It is faster than other APIs developed with Asp.Net Core MVC.
- **Easy**: Just worry about writing code for web.
- **Portable**: It is quite similar to other platforms such as [expressjs](https://expressjs.com/). So you can easily transfer your existing code to RoutingRecords..
- **Cool APIs**: All these features together makes your code look cool.

Take a look at this example:

```csharp
public record UpdateItem(IItemStore store)
    : Put("items/{id:int}", async (req, res) =>
    {
        var id = req.FromRoute<int>("id");
        var item = await req.FromJsonAsync<Item>();
        if (item == null)
        {
            res.Status(Status400BadRequest);
            return;
        }

        await store.UpsertAsync(id, item);
        await res.JsonAsync(item);
    });
```

Isn't it cool?

## Quick start

Create a new web project:

```bash
mkdir MyApp
cd MyApp
dotnet new web
```

First of all, you have to add `RoutingRecords` package reference to your project:

```bash
dotnet add package RoutingRecords
```

Then you have to call `AddRouteRecords` in the `ConfigureServices` and `MapRouteRecords` in the enpoints mapping section:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RoutingRecords;

namespace SampleApp
{
    public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRouteRecords(typeof(Startup).Assembly);
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
	}
}
```

Now you can create a new file with your route `record`:

```csharp
using RoutingRecords;

namespace SampleApp.Api
{
    public record Hello()
        : Get("/", (req, res) =>
            res.SendAsync("Welcome to RoutingRecords"));
}
```

Finally test your application:

```bash
dotnet run
```

## Route Records

There is a `record` for each element defined in the `HttpMethods` enum:

```csharp
Connect(string Pattern, RouteDelegateAsync RouteDelegate);

Delete(string Pattern, RouteDelegateAsync RouteDelegate);

Get(string Pattern, RouteDelegateAsync RouteDelegate);

Head(string Pattern, RouteDelegateAsync RouteDelegate);

Options(string Pattern, RouteDelegateAsync RouteDelegate);

Patch(string Pattern, RouteDelegateAsync RouteDelegate);

Post(string Pattern, RouteDelegateAsync RouteDelegate);

Put(string Pattern, RouteDelegateAsync RouteDelegate);

Trace(string Pattern, RouteDelegateAsync RouteDelegate);
```

If you want to use them, you just have to create an object that inherits from one of these records, specify the pattern and declare the delegate.

The delegate is a function that returns a Task (async function) with a `HttpRequest` and a `HttpResponse` as parameters:

```csharp
Task RouteDelegateAsync(HttpRequest req, HttpResponse res);
```

## Extensions

RoutingRecords adds some features related with API development:

### HttpRequest:

These are the methods to extend the `HttpRequest` behavior:

```csharp
Task<T> FromJsonAsync<T>(bool validateMediaType = false, CancellationToken cancellationToken = default);

T FromRoute<T>(string name);

T FromQuery<T>(string name);

bool TryFromRoute<T>(string name, out T result);

bool TryFromQuery<T>(string name, out T result);
```

- **FromJsonAsync**: Deserializes the request body using `System.Text.json.JsonSerializer`. If `validateMediaType` is set to `true` it throws an exception when the media type of the request it's not "application/json". If you don't specify `cancellationToken` it uses the current `HttpContext.RequestAborted` value.
- **FromRoute**: Gets a value from the request route values.
- **FromQuery**: Gets a value from the request query string values.
- **TryFromRoute**: Tries to get a value from the request route values. Returns `false` if it doesn't exists.
- **TryFromQuery**: Tries to get a value from the request query string values. Returns `false` if it doesn't exists.

### HttpResponse:

These are the methods to extend the `HttpResponse` behavior:

```csharp
Task SendAsync(string body, CancellationToken cancellationToken = default);

Task SendAsync(string body, string mimeType, CancellationToken cancellationToken = default);

Task JsonAsync<T>(T body, CancellationToken cancellationToken = default);

HttpResponse Status(int statusCode);

HttpResponse Status(HttpStatusCode statusCode);
```

- **SendAsync**: Writes a string in the response boy. The `mimeType` default value is "text/plain". If you don't specify `cancellationToken` it uses the current `HttpContext.RequestAborted` value.
- **JsonAsync**: Writes an objet serialized as json in the response body. If you don't specify `cancellationToken` it uses the current `HttpContext.RequestAborted` value.
- **Status**: Sets the response HTTP status code.

## Authorization

RoutingRoutes is full integrated with Asp.Net Authorization framework. You can add a global policy with an authorization requirement for all enpoints:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddRouteRecords();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .Add...(options => { ... });

    services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
}
```

In the same way you get a `IEndpointConventionBuilder` when you are registering an endpoint, when you are mapping objects of type `RouteRecord`, you will get an object of type:

- `IRecordEndpointConventionBuilder` for one record map call (`MapRouteRecord`).
- `IRecordEndpointConventionBuilderCollection` for multiple records map call (`MapRouteRecords`).

```csharp
interface IRecordEndpointConventionBuilder
    : IEndpointConventionBuilder
{
    Type RouteRecordType { get; }
}

interface IRecordEndpointConventionBuilderCollection
	: IEndpointConventionBuilder, IEnumerable<IRecordEndpointConventionBuilder>
{
}
```

So you can use the convention builders for every `RouteRecord`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddRouteRecords();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .Add...(options => { ... });

    services.AddAuthorization();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints
        => endpoints.MapRouteRecords()
                    .Where(x => x.RouteRecordType.IsNot<Hello>())
                    .ToList()
                    .ForEach(y => y.RequireAuthorization()));
}
```

## Benchmarks

We have run the benchmarks you can find in this repository in an Azure Virtual Machine. We have used Ubuntu 20.10 as OS and D2v3 as size:

```bash
BenchmarkDotNet=v0.12.1, OS=ubuntu 20.10
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.202
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
  DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
```

After 5 runnings these are de average values we got:

| Tool              | Method   |     Mean |   %99.9 | Ratio | Allocated |
|--------------     |-------   |---------:|--------:|------:|----------:|
| **Mvc**           |   *POST* | 288.8 us | 8.49 us |  1.00 |  20.04 KB |
| **Route Records** |   *POST* | 167.8 us | 4.72 us |  0.59 |  16.01 KB |
|                   |          |          |         |       |           |
| **Mvc**           |    *GET* | 281.1 us | 5.59 us |  1.00 |  21.27 KB |
| **Route Records** |    *GET* | 257.7 us | 5.12 us |  0.92 |  19.35 KB |

## CRUD example

We are going to define a full CRUD example using RoutingRecords to show how it feels:

### Create

```csharp
using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record CreateTodo(ITodoStore store)
        : Post("todos", async (req, res) =>
        {
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.Status(Status400BadRequest);
                return;
            }

            await store.InsertAsync(todo);

            await res
                    .Status(Status201Created)
                    .JsonAsync(new
                    {
                        Ref = $"todos/{store.Counter}"
                    });
        });
}
```

### Read All

```csharp
using System.Linq;
using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record ReadTodos(ITodoStore store)
        : Get("todos", async (req, res) =>
        {
            var todos = await store.GetAllAsync();
            if (!todos.Any())
            {
                res.Status(Status204NoContent);
                return;
            }

            await res.JsonAsync(todos);
        });
}
```

### Read One

```csharp
using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record ReadTodo(ITodoStore store)
        : Get("todos/{id:int}", async (req, res) =>
        {
            var id = int.Parse((string)req.RouteValues["id"]);
            var todo = await store.GetOneAsync(id);
            if (todo == null)
            {
                res.Status(Status404NotFound);
                return;
            }

            await res.JsonAsync(todo);
        });
}
```

### Update

```csharp
using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record UpdateTodo(ITodoStore store)
        : Put("todos/{id:int}", async (req, res) =>
        {
            var id = req.FromRoute<int>("id");
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.Status(Status400BadRequest);
                return;
            }

            await store.UpsertAsync(id, todo);
            await res.JsonAsync(todo);
        });
}
```

### Delete

```csharp
using System.Threading.Tasks;
using RoutingRecords;

namespace SampleApp.Api.Todos
{
    public record DeleteTodo(ITodoStore store)
        : Delete("todos/{id:int}", async (req, res) =>
        {
            var id = req.FromRoute<int>("id");
            await store.DeleteAsync(id);
        });
}
```

## License

The source code we develop at **RoutingRecords** is default being licensed as CC-BY-SA-4.0. You can read more about [here](LICENSE.md).
