using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstApproach
{
	public delegate Task RouteDelegateAsync(HttpRequest req, HttpResponse res);

	public abstract record RouteRecord(string Pattern, string Verb, RouteDelegateAsync RouteDelegate);

	public abstract record Get(string Pattern, RouteDelegateAsync RouteDelegate)
	  : RouteRecord(Pattern, HttpMethods.Get, RouteDelegate);

	public abstract record Put(string Pattern, RouteDelegateAsync RouteDelegate)
	  : RouteRecord(Pattern, HttpMethods.Put, RouteDelegate);

	public abstract record Post(string Pattern, RouteDelegateAsync RouteDelegate)
	  : RouteRecord(Pattern, HttpMethods.Post, RouteDelegate);

	public abstract record Delete(string Pattern, RouteDelegateAsync RouteDelegate)
	  : RouteRecord(Pattern, HttpMethods.Delete, RouteDelegate);

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRouteRecords(this IServiceCollection services)
		{
			Assembly
				.GetEntryAssembly()
				.GetTypes()
				.Where(type => !type.IsAbstract && typeof(RouteRecord).IsAssignableFrom(type))
				.ToList()
				.ForEach(type =>
				{
					services.AddScoped(type);
					services.AddScoped(s => (RouteRecord)s.GetService(type));
				});

			return services;
		}
	}

	public static class EndpointRouteBuilderExtensions
	{
		public static IEndpointRouteBuilder MapRouteRecords(this IEndpointRouteBuilder endpoints)
		{
			using var scope = endpoints.ServiceProvider.CreateScope();
			scope.ServiceProvider
			     .GetServices<RouteRecord>()
			     .ToList()
			     .ForEach(route =>
			     {
				     var type = route.GetType();
				     endpoints.MapMethods(
				  	 pattern: route.Pattern,
				  	 httpMethods: new[] { route.Verb },
				  	 requestDelegate: ctx =>
				  	 {
				  		 var r = (RouteRecord)ctx.RequestServices.GetService(type);
				  		 return r.RouteDelegate(ctx.Request, ctx.Response);
				  	 });
			     });

			return endpoints;
		}
	}

	public static class HttpResponseExtensions
	{
		private static readonly JsonSerializerOptions _jsonOptions
			= new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

		public static Task JsonAsync<T>(this HttpResponse res, T body)
			=> JsonSerializer.SerializeAsync(res.Body, body, _jsonOptions);
	}

	public static class HttpRequestExtensions
	{
		private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		public static ValueTask<T> FromJsonAsync<T>(this HttpRequest req)
			=> JsonSerializer.DeserializeAsync<T>(req.Body, _jsonOptions);

		public static T FromRoute<T>(this HttpRequest req, string name)
		{
			var s = (string)req.RouteValues[name];
			var converter = TypeDescriptor.GetConverter(typeof(T));
			return (T)converter.ConvertFrom(s);
		}
	}
}
