namespace RoutingRecords;

internal class ResultBuilder : IResultBuilder
{
    private int? _statusCode = null;
    private string _mediaType = null;
    private Func<HttpResponse, Task> _bodyWriter = r => Task.CompletedTask;

    public Task ExecuteAsync(HttpContext ctx)
    {
        if (!string.IsNullOrWhiteSpace(_mediaType))
        {
            ctx.Response.ContentType = _mediaType;
        }

        if (_statusCode.HasValue)
        {
            ctx.Response.StatusCode = _statusCode.Value;
        }

        return _bodyWriter(ctx.Response);
    }

    public IResultBuilder Json<T>(T body)
    {
        _bodyWriter = res => res.JsonAsync(body);
        return this;
    }

    public IResultBuilder Send(string body)
    {
        _bodyWriter = res => res.SendAsync(body);
        return this;
    }

    public IResultBuilder Send(string body, string mediaType)
    {
        _mediaType = mediaType;
        _bodyWriter = res => res.SendAsync(body);
        return this;
    }

    public IResultBuilder SendFile(IFileInfo fileInfo)
    {
        _bodyWriter = res => res.SendFileAsync(fileInfo);
        return this;
    }

    public IResultBuilder SendFile(string filename)
    {
        _bodyWriter = res => res.SendFileAsync(filename);
        return this;
    }

    public IResultBuilder Status(int statusCode)
    {
        _statusCode = statusCode;
        return this;
    }

    public IResultBuilder Status(HttpStatusCode statusCode)
    {
        _statusCode = (int)statusCode;
        return this;
    }
}
