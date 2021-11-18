using Microsoft.AspNetCore.Routing.Patterns;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Adds every <see cref="RouteRecord"/> endpoints to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
    /// <returns>A <see cref="IRecordEndpointConventionBuilderCollection" /> that can be used to further customize the endpoints.</returns>
    public static IEndpointConventionBuilder MapRouteRecords(this IEndpointRouteBuilder endpoints)
    {
        using var scope = endpoints.ServiceProvider.CreateScope();
        var builders = scope.ServiceProvider
                            .GetServices<RouteRecord>()
                             .Select(route => endpoints.MapRouteRecord(scope.ServiceProvider, route));

        return new RecordEndpointConventionBuilderCollection(builders);
    }

    /// <summary>
    /// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
    /// <param name="route">The <see cref="RouteRecord" /> to add.</param>
    /// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
    public static IEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, RouteRecord route)
    {
        using var scope = endpoints.ServiceProvider.CreateScope();
        return endpoints.MapRouteRecord(scope.ServiceProvider, route);
    }

    /// <summary>
    /// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
    /// </summary>
    /// <typeparam name="T">The <see cref="RouteRecord" /> type to add.</typeparam>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
    /// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
    public static IEndpointConventionBuilder MapRouteRecord<T>(this IEndpointRouteBuilder endpoints) where T : RouteRecord
        => endpoints.MapRouteRecord(typeof(T));

    /// <summary>
    /// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
    /// <param name="type">The <see cref="RouteRecord" /> type to add.</typeparam>
    /// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
    public static IEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, Type type)
    {
        if (!typeof(RouteRecord).IsAssignableFrom(type))
        {
            throw new InvalidCastException($"The type '{type}' is not a RouteRecord");
        }

        using var scope = endpoints.ServiceProvider.CreateScope();
        var route = (RouteRecord)scope.ServiceProvider.GetRequiredService(type);

        return endpoints.MapRouteRecord(scope.ServiceProvider, route);
    }

    private static IEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, IServiceProvider serviceProvider, RouteRecord route)
    { 
        var requestDelegate = CreateDelegate(serviceProvider, route);
        var builder = endpoints.Map(RoutePatternFactory.Parse(route.Pattern), requestDelegate);
        builder.WithDisplayName($"{ route.Pattern } HTTP: { route.Verb }");
        builder.WithMetadata(new HttpMethodMetadata(new[] { route.Verb }));
        builder.WithMetadata(route.Delegate.GetMethodInfo());
        builder.WithMetadata(route.GetType().GetCustomAttributes(true));
        return builder;
    }
        

    private static RequestDelegate CreateDelegate(IServiceProvider serviceProvider, RouteRecord route)
    {
        var requestDelegateBuilder = serviceProvider.GetRequiredService<IRequestDelegateBuilder>();
        return requestDelegateBuilder.CreateFor(route);
    }
}
