namespace RoutingRecords;

public interface IResultBuilder : IResult
{
    IResultBuilder Status(int statusCode);

    IResultBuilder Status(HttpStatusCode statusCode);

    IResultBuilder Send(string body);

    IResultBuilder Send(string body, string mediaType);

    IResultBuilder SendFile(IFileInfo body);

    IResultBuilder SendFile(string filename);

    IResultBuilder Json<T>(T body);
}
