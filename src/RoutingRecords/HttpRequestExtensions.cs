namespace RoutingRecords;

public static class HttpRequestExtensions
{
    /// <summary>
    /// Gets a value of type <see cref="T"/> from the route values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the route value.</param>
    /// <returns>An object of type <see cref="T"/> found in the route value collection of the <see cref="HttpRequest"/>.</returns>
    public static T FromRoute<T>(this HttpRequest req, string name)
        => req.RouteValues[name].As<T>();


    /// <summary>
    /// Gets a value of type <see cref="T"/> from the query values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the query value.</param>
    /// <returns>An object of type <see cref="T"/> found in the query value collection of the <see cref="HttpRequest"/>.</returns>
    public static T FromQuery<T>(this HttpRequest req, string name)
        => req.Query[name].As<T>();

    /// <summary>
    /// Gets a value of type <see cref="T"/> from the header values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the header value.</param>
    /// <returns>An object of type <see cref="T"/> found in the header value collection of the <see cref="HttpRequest"/>.</returns>
    public static T FromHeader<T>(this HttpRequest req, string name)
        => req.Headers[name].As<T>();

    /// <summary>
    /// Gets a value of type <see cref="type"/> from the route values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the route value.</param>
    /// <returns>An object of type <see cref="type"/> found in the route value collection of the <see cref="HttpRequest"/>.</returns>
    public static object FromRoute(this HttpRequest req, Type type, string name)
        => req.RouteValues[name].As(type);

    /// <summary>
    /// Gets a value of type <see cref="type"/> from the query values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the query value.</param>
    /// <returns>An object of type <see cref="type"/> found in the query value collection of the <see cref="HttpRequest"/>.</returns>
    public static object FromQuery(this HttpRequest req, Type type, string name)
        => req.Query[name].As(type);

    /// <summary>
    /// Gets a value of type <see cref="type"/> from the header values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the header value.</param>
    /// <returns>An object of type <see cref="type"/> found in the header value collection of the <see cref="HttpRequest"/>.</returns>
    public static object FromHeader(this HttpRequest req, Type type, string name)
        => req.Headers[name].As(type);


    /// <summary>
    /// Tries to get a value of type <see cref="T"/> from the route values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the route value.</param>
    /// <param name="result">An object of type <see cref="T"/> found in the route value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the route value exists.</returns>
    public static bool TryFromRoute<T>(this HttpRequest req, string name, out T result)
    {
        result = req.RouteValues.ContainsKey(name) ? req.RouteValues[name].As<T>() : default;
        return req.RouteValues.ContainsKey(name);
    }

    /// <summary>
    /// Tries to get a value of type <see cref="T"/> from the query values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the query value.</param>
    /// <param name="result">An object of type <see cref="T"/> found in the query value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the query value exists.</returns>
    public static bool TryFromQuery<T>(this HttpRequest req, string name, out T result)
    {
        result = req.Query.ContainsKey(name) ? req.Query[name].As<T>() : default;
        return req.Query.ContainsKey(name);
    }

    /// <summary>
    /// Tries to get a value of type <see cref="T"/> from the header values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="name">The name of the header value.</param>
    /// <param name="result">An object of type <see cref="T"/> found in the header value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the header value exists.</returns>
    public static bool TryFromHeader<T>(this HttpRequest req, string name, out T result)
    {
        result = req.Headers.ContainsKey(name) ? req.Headers[name].As<T>() : default;
        return req.Headers.ContainsKey(name);
    }

    /// <summary>
    /// Tries to get a value of type <see cref="type"/> from the route values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the route value.</param>
    /// <param name="result">An object of type <see cref="type"/> found in the route value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the route value exists.</returns>
    public static bool TryFromRoute(this HttpRequest req, Type type, string name, out object result)
    {
        result = req.RouteValues.ContainsKey(name) ? req.RouteValues[name].As(type) : default;
        return req.RouteValues.ContainsKey(name);
    }

    /// <summary>
    /// Tries to get a value of type <see cref="type"/> from the query values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the query value.</param>
    /// <param name="result">An object of type <see cref="type"/> found in the query value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the query value exists.</returns>
    public static bool TryFromQuery(this HttpRequest req, Type type, string name, out object result)
    {
        result = req.Query.ContainsKey(name) ? req.Query[name].As(type) : default;
        return req.Query.ContainsKey(name);
    }

    /// <summary>
    /// Tries to get a value of type <see cref="type"/> from the header values for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The <see cref="HttpRequest"/>.</param>
    /// <param name="type">The type of the value.</typeparam>
    /// <param name="name">The name of the header value.</param>
    /// <param name="result">An object of type <see cref="type"/> found in the header value collection of the <see cref="HttpRequest"/>.</param>
    /// <returns>If the header value exists.</returns>
    public static bool TryFromHeader(this HttpRequest req, Type type, string name, out object result)
    {
        result = req.Headers.ContainsKey(name) ? req.Headers[name].As(type) : default;
        return req.Headers.ContainsKey(name);
    }

    private static T As<T>(this object obj)
        => (T)(obj.As(typeof(T)) ?? default(T));


    private static object As(this object obj, Type type)
    {
        if (obj == null) return default;
        if (type.IsAssignableFrom(obj.GetType())) return obj;
        if (obj is StringValues sv) obj = sv.ToString();
        if (obj is string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return default;

            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(typeof(string))) return converter.ConvertFrom(s);
        }

        return default;
    }
}
