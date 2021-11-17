namespace RoutingRecords;

public static class HttpResponseExtensions
{
    /// <summary>
    /// Writes a string with type "text/plain" in a <see cref="HttpResponse"/> body.
    /// </summary>
    /// <param name="res">The <see cref="HttpResponse"/>.</param>
    /// <param name="body">The string to write.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the write operation.</param>
    /// <returns></returns>
    public static Task SendAsync(this HttpResponse res, string body, CancellationToken cancellationToken = default)
    {
        return res.SendAsync(body, MediaTypeNames.Text.Plain, cancellationToken);
    }

    /// <summary>
    /// Writes a string with specified media type in a <see cref="HttpResponse"/> body.
    /// </summary>
    /// <param name="res">The <see cref="HttpResponse"/>.</param>
    /// <param name="body">The string to write.</param>
    /// <param name="mimeType">The media type.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the write operation.</param>
    /// <returns></returns>
    public static Task SendAsync(this HttpResponse res, string body, string mimeType, CancellationToken cancellationToken = default)
    {
        cancellationToken = cancellationToken == default ? res.HttpContext.RequestAborted : cancellationToken;
        cancellationToken.ThrowIfCancellationRequested();

        res.ContentType = mimeType;
        var data = Encoding.UTF8.GetBytes(body);
        return res.Body.WriteAsync(data, 0, data.Length, cancellationToken);
    }

    /// <summary>
    /// Sets a <see cref="HttpResponse"/> status code to the specified <see cref="int"/>.
    /// </summary>
    /// <param name="res">The <see cref="HttpResponse"/>.</param>
    /// <param name="statusCode">The status code to set.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static HttpResponse Status(this HttpResponse res, int statusCode)
    {
        res.StatusCode = statusCode;
        return res;
    }

    /// <summary>
    /// Sets a <see cref="HttpResponse"/> status code to the specified <see cref="HttpStatusCode"/>.
    /// </summary>
    /// <param name="res">The <see cref="HttpResponse"/>.</param>
    /// <param name="statusCode">The <see cref="HttpStatusCode"/> to set.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static HttpResponse Status(this HttpResponse res, HttpStatusCode statusCode)
    {
        res.StatusCode = (int)statusCode;
        return res;
    }
}
