namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Builds conventions that will be used for customization of <see cref="RoutingRecords.RouteRecord"/> instances.
/// </summary>
public interface IRecordEndpointConventionBuilder : IEndpointConventionBuilder
{
    /// <summary>
    /// Gets the associated <see cref="RoutingRecords.RouteRecord"/> object type.
    /// </summary>
    Type RouteRecordType { get; }
}
