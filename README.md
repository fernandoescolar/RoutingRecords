# RoutingRecords

RoutingRecords is a small set of tools that help Asp.Net Core developers to program cool and simple APIs in .Net 5. It relies on `record` type objects to define inline endpoints:

```csharp
record Hello()
  : Get("/", (req, res) =>
      res.SendAsync("Welcome to RoutingRecords"));
```

Main features:

- **Minimalistic**: You can define an endpoint in a single line of code.
- **Fast**: It is faster than other APIs developed with Asp.Net Core MVC.
- **Easy**: Just worry about writing code for web.
- **Portable**: It is quite similar to other platforms such as [expressjs](https://expressjs.com/). So you can easily transfer your existing code to RoutingRecords.
- **Cool APIs**: All these features together makes your code look cool.

Take a look at this example:

```csharp
record UpdateItem(IItemStore store)
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

## Why record?

The `record` keyword is a new language feature in C# 9. It declares an impressive object because:

- It is immutable by default.
- You can declare it on a single line.
- You can inject external dependencies into its constructor.
- It is very readable.

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

Then you have two choices:

### Single source code file

Delete all .cs files and add a new one with this content:

```csharp
using RoutingRecords;

new ApiApp().Run();

record Hello() : Get("/", (req, res) =>
   res.SendAsync("Hello World!"));
```

And test your application:

```bash
dotnet run
```

> **Disclaimer**: this solution is great for quick little APIs. But its use is not recommended in production applications.

### Traditional way

In the StartUp file you have to call `AddRouteRecords` in the `ConfigureServices` and `MapRouteRecords` in the enpoints mapping section:

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
        res.SendAsync("Hello World!"));
}
```

Finally test your application:

```bash
dotnet run
```

## Documentation

- Tools
  - [Route types](https://github.com/fernandoescolar/RoutingRecords/wiki/Route-types)
  - [HttpRequest extensions](https://github.com/fernandoescolar/RoutingRecords/wiki/HttpRequest-extensions)
  - [HttResponse extensions](https://github.com/fernandoescolar/RoutingRecords/wiki/HttResponse-extensions)
  - [ApiApp](https://github.com/fernandoescolar/RoutingRecords/wiki/ApiApp)
- [CRUD Example](https://github.com/fernandoescolar/RoutingRecords/wiki/CRUD-Example)
- [Authorization](https://github.com/fernandoescolar/RoutingRecords/wiki/Authorization)

## Benchmarks

We have run the benchmarks you can find in this repository on an Azure virtual machine. We have used Debian 10 as operating system and D2v3 as size:

```bash
BenchmarkDotNet=v0.12.1, OS=debian 10
Intel Xeon Platinum 8171M CPU 2.60GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.202
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
  DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
```

After 7 runs, these are the average values we have obtained:

| Tool              | Method     |      Mean |    %99.9 | Ratio | Allocated |
|------------------ |----------- |----------:|---------:|------:|----------:|
| **Mvc**           |        Add | 140.92 us | 3.502 us |  1.00 |  20.08 KB |
| **Route Records** |        Add |  91.01 us | 1.766 us |  0.67 |  15.97 KB |
|                   |            |           |          |       |           |
| **Mvc**           |        Get |  70.81 us | 1.363 us |  1.00 |  13.04 KB |
| **Route Records** |        Get |  55.18 us | 1.087 us |  0.78 |  10.95 KB |

What this means is RoutingRecords does less "extra" work than Asp.Net Core MVC. That's why it' it's faster and lighter. It might be enough for your API. But if you miss some features, we recommend you to use Asp.Net Core MVC.

You can find the complete result [here](https://github.com/fernandoescolar/RoutingRecords/wiki/Benchmarks).

## License

The source code we develop at **RoutingRecords** is default being licensed as MIT. You can read more about [here](LICENSE).
