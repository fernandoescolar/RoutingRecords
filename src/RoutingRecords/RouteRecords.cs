namespace RoutingRecords;

/// <summary>
/// A <see cref="RouteRecord"/> delegate. This is the functions to be executed in the route.
/// </summary>
/// <param name="req">The current <see cref="HttpRequest"/>.</param>
/// <param name="res">The current <see cref="HttpResponse"/>.</param>
/// <returns></returns>
public delegate Task RouteDelegate(HttpRequest req, HttpResponse res);

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Connect"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Connect(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Connect, Delegate)
{
    public Connect(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {

    }

    public Connect(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Delete"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Delete(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Delete, Delegate)
{
    public Delete(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Delete(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Get"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Get(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Get, Delegate)
{
    public Get(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Get(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Head"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Head(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Head, Delegate)
{
    public Head(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Head(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Options"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Options(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Options, Delegate)
{
    public Options(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Options(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Patch"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Patch(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Patch, Delegate)
{
    public Patch(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Patch(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Post"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Post(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Post, Delegate)
{
    public Post(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Post(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Put"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Put(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Put, Delegate)
{
    public Put(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Put(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}

/// <summary>
/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Trace"/> method.
/// </summary>
/// <param name="Pattern"> A string representation of the route pattern.</param>
/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
public abstract record Trace(string Pattern, Delegate Delegate) : RouteRecord(Pattern, HttpMethods.Trace, Delegate)
{
    public Trace(string Pattern, RequestDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }

    public Trace(string Pattern, RouteDelegate @delegate) : this(Pattern, (Delegate)@delegate)
    {
    }
}
