namespace RoutingRecords;

public static class HttpRequestJsonExtensions
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Reads an object of type <see cref="T"/> deserialized from json content of the specified <see cref="HttpRequest"/> body.
    /// </summary>
    /// <typeparam name="T">The object type to deserialize.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="validateMediaType">If <see cref="true"/> it throws an <see cref="InvalidMediaTypeException"/> if the media type of the <see cref="HttpRequest"/> body is not "application/json".</param>
    /// <param name="cancellationToken">A token that may be used to cancel the read operation.</param>
    /// <returns>The deserialized object of type <see cref="T"/>.</returns>
    public static async Task<T> FromJsonAsync<T>(this HttpRequest req, bool validateMediaType = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = cancellationToken == default ? req.HttpContext.RequestAborted : cancellationToken;
        cancellationToken.ThrowIfCancellationRequested();
        CheckRequestMediaType(req, validateMediaType);

        try
        {
            return await JsonSerializer.DeserializeAsync<T>(req.Body, _jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            var logger = req.HttpContext.RequestServices.GetService<ILogger<HttpRequest>>();
            logger.LogError($"Cannot deserialize Json: {ex.Message}");

            return default;
        }
    }

    /// <summary>
    /// Reads an object of type <see cref="type"/> deserialized from json content of the specified <see cref="HttpRequest"/> body.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The object type to deserialize.</typeparam>
    /// <param name="validateMediaType">If <see cref="true"/> it throws an <see cref="InvalidMediaTypeException"/> if the media type of the <see cref="HttpRequest"/> body is not "application/json".</param>
    /// <param name="cancellationToken">A token that may be used to cancel the read operation.</param>
    /// <returns>The deserialized object of type <see cref="type"/>.</returns>
    public static async Task<object> FromJsonAsync(this HttpRequest req, Type type, bool validateMediaType = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = cancellationToken == default ? req.HttpContext.RequestAborted : cancellationToken;
        cancellationToken.ThrowIfCancellationRequested();
        CheckRequestMediaType(req, validateMediaType);

        try
        {
            return await JsonSerializer.DeserializeAsync(req.Body, type, _jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            var logger = req.HttpContext.RequestServices.GetService<ILogger<HttpRequest>>();
            logger.LogError($"Cannot deserialize Json: {ex.Message}");

            return default;
        }
    }

    private static void CheckRequestMediaType(HttpRequest req, bool validateMediaType)
    {
        if (!req.ContentType.StartsWith(MediaTypeNames.Application.Json))
        {
            var message = $"Expected '{MediaTypeNames.Application.Json}' content-type. Found: '{req.ContentType}'";
            if (validateMediaType)
            {
                throw new InvalidMediaTypeException(message);
            }
            else
            {
                var logger = req.HttpContext.RequestServices.GetService<ILogger<HttpRequest>>();
                logger.LogWarning(message);
            }
        }
    }
}
