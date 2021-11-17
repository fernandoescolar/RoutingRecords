namespace RoutingRecords;

/// <summary>
/// Represents an instance <see cref="Microsoft.AspNetCore.Routing.RouteEndpoint"/> using <see cref="record"/> types
/// that matches HTTP requests for the specified HTTP methods and pattern.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Verb">The HTTP method that the endpoint will match.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record RouteRecord(string Pattern, string Verb, Delegate Delegate)
{
    public static IResultBuilder Json<T>(T body)
        => new ResultBuilder().Json(body);

    public static IResultBuilder Send(string body)
        => new ResultBuilder().Send(body);

    public static IResultBuilder Send(string body, string mediaType)
        => new ResultBuilder().Send(body, mediaType);

    public static IResultBuilder SendFile(IFileInfo body)
        => new ResultBuilder().SendFile(body);

    public static IResultBuilder SendFile(string filename)
        => new ResultBuilder().SendFile(filename);

    public static IResultBuilder Status(int statusCode)
        => new ResultBuilder().Status(statusCode);

    public static IResultBuilder Status(HttpStatusCode statusCode)
        => new ResultBuilder().Status(statusCode);
}
