namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds services for objecs of type <see cref="RouteRecord"/> to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="assemblies">The assemblies where it will look for object of type <see cref="RouteRecord"/>.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddRouteRecords(this IServiceCollection services, params Assembly[] assemblies)
    {
        assemblies
                .SelectMany(a =>
                    a.GetTypes()
                     .Where(type => !type.IsAbstract && typeof(RouteRecord).IsAssignableFrom(type))
                )
                .ToList()
                .ForEach(type =>
                {
                    services.AddScoped(type);
                    services.AddScoped(s => (RouteRecord)s.GetService(type));
                });

        return services.AddRouteRecordDefaults();
    }

    /// <summary>
    /// Adds services for objecs of type <see cref="RouteRecord"/> to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <remarks>It will look for objects of type <see cref="RouteRecord"/> in all loaded assemblies.</remarks>
    public static IServiceCollection AddRouteRecords(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetEntryAssembly();
        var assemblies = currentAssembly
                                 .GetReferencedAssemblies()
                                 .Select(Assembly.Load)
                                 .ToList();

        if (!assemblies.Contains(currentAssembly))
        {
            assemblies.Add(Assembly.GetEntryAssembly());
        }

        return services.AddRouteRecords(assemblies.ToArray());
    }

    private static IServiceCollection AddRouteRecordDefaults(this IServiceCollection services)
    {
        services.AddTransient<IRequestDelegateBuilder, RequestDelegateBuilder>();

        services.AddTransient<IRequestDelegateConverter, FromRequestDelegate>();
        services.AddTransient<IRequestDelegateConverter, FromRouteDelegate>();
        services.AddTransient<IRequestDelegateConverter, FromDefaultDelegate>();

        services.AddTransient<IParameterBinder, FromBodyBinder>();
        services.AddTransient<IParameterBinder, FromHeaderBinder>();
        services.AddTransient<IParameterBinder, FromQueryBinder>();
        services.AddTransient<IParameterBinder, HttpContextBinder>();
        services.AddTransient<IParameterBinder, HttpRequestBinder>();
        services.AddTransient<IParameterBinder, HttpResponseBinder>();
        services.AddTransient<IParameterBinder, DefaultBinder>();

        services.AddTransient<IResponseProcessor, FromTaskWithResultProcessor>();
        services.AddTransient<IResponseProcessor, FromTaskProcessor>();
        services.AddTransient<IResponseProcessor, FromResultProcessor>();
        services.AddTransient<IResponseProcessor, EmptyResponseProcessor>();

        return services;
    }
}
