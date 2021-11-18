namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Builds conventions that will be used for customization of a <see cref="RoutingRecords.RouteRecord"/> instance collection.
/// </summary>
public interface IRecordEndpointConventionBuilderCollection
    : IEndpointConventionBuilder, IEnumerable<IEndpointConventionBuilder>
{
}
