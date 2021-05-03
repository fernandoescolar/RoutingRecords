using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RoutingRecords
{
	/// <summary>
	/// A <see cref="RouteRecord"/> delegate. This is the functions to be executed in the route.
	/// </summary>
	/// <param name="req">The current <see cref="HttpRequest"/>.</param>
	/// <param name="res">The current <see cref="HttpResponse"/>.</param>
	/// <returns></returns>
	public delegate Task RouteDelegateAsync(HttpRequest req, HttpResponse res);

	/// <summary>
	/// Represents an instance <see cref="Microsoft.AspNetCore.Routing.RouteEndpoint"/> using <see cref="record"/> types
	/// that matches HTTP requests for the specified HTTP methods and pattern.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="Verb">The HTTP method that the endpoint will match.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record RouteRecord(string Pattern, string Verb, RouteDelegateAsync RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Connect"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Connect(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Connect, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Delete"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Delete(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Delete, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Get"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Get(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Get, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Head"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Head(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Head, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Options"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Options(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Options, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Patch"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Patch(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Patch, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Post"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Post(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Post, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Put"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Put(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Put, RouteDelegate);

	/// <summary>
	/// Represents a route that matches HTTP requests for the specified pattern and the <see cref="HttpMethods.Trace"/> method.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="RouteDelegate">The delegate executed when the endpoint is matched.</param>
	public abstract record Trace(string Pattern, RouteDelegateAsync RouteDelegate)
		: RouteRecord(Pattern, HttpMethods.Trace, RouteDelegate);
}
