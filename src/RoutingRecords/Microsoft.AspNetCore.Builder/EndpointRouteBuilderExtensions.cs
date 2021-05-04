using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using RoutingRecords;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
	public static class EndpointRouteBuilderExtensions
	{
		/// <summary>
		/// Adds every <see cref="RouteRecord"/> endpoints to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
		/// <returns>A <see cref="IRecordEndpointConventionBuilderCollection" /> that can be used to further customize the endpoints.</returns>
		public static IRecordEndpointConventionBuilderCollection MapRouteRecords(this IEndpointRouteBuilder endpoints)
		{
			using var scope = endpoints.ServiceProvider.CreateScope();
			var builders = scope.ServiceProvider
									.GetServices<RouteRecord>()
				 				.Select(handler => endpoints.MapRouteRecord(handler));

			return new RecordEndpointConventionBuilderCollection(builders);
		}

		/// <summary>
		/// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
		/// <param name="route">The <see cref="RouteRecord" /> to add.</param>
		/// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
		public static IRecordEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, RouteRecord route)
			=> endpoints.MapRouteRecord(route.Pattern, route.Verb, route.GetType());

		/// <summary>
		/// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
		/// </summary>
		/// <typeparam name="T">The <see cref="RouteRecord" /> type to add.</typeparam>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
		/// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
		public static IRecordEndpointConventionBuilder MapRouteRecord<T>(this IEndpointRouteBuilder endpoints) where T : RouteRecord
			=> endpoints.MapRouteRecord(typeof(T));

		/// <summary>
		/// Adds a <see cref="RouteRecord"/> endpoint to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
		/// <param name="type">The <see cref="RouteRecord" /> type to add.</typeparam>
		/// <returns>A <see cref="IRecordEndpointConventionBuilder" /> that can be used to further customize the endpoint.</returns>
		public static IRecordEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, Type type)
		{
			if (!typeof(RouteRecord).IsAssignableFrom(type))
			{
				throw new InvalidCastException($"The type '{type}' is not a RouteRecord");
			}

			(var pattern, var verb) = GetRoutePatternAndVerb(endpoints, type);
			return endpoints.MapRouteRecord(pattern, verb, type);
		}

		private static IRecordEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, string pattern, string verb, Type type)
			=> endpoints.MapMethod(pattern,
								   verb,
								   type,
								   ctx => RouteRecordHandler(ctx, type));

		private static IRecordEndpointConventionBuilder MapMethod(this IEndpointRouteBuilder endpoints, string pattern, string verb, Type type, RequestDelegate requestDelegate)
			=> new RecordEndpointConventionBuilder(type, endpoints.MapMethods(pattern, new[] { verb }, requestDelegate));

		private static Task RouteRecordHandler(HttpContext ctx, Type type)
		{
			var r = (RouteRecord)ctx.RequestServices.GetService(type);
			return r.RouteDelegate(ctx.Request, ctx.Response);
		}

		private static (string pattern, string verb) GetRoutePatternAndVerb(IEndpointRouteBuilder endpoints, Type type)
		{
			var route = InstanciateFromParameterLessContructor(type);
			if (route != default)
			{
				return (route.Pattern, route.Verb);
			}

			using var scope = endpoints.ServiceProvider.CreateScope();
			route = (RouteRecord)scope.ServiceProvider.GetRequiredService(type);
			return (route.Pattern, route.Verb);
		}

		private static RouteRecord InstanciateFromParameterLessContructor(Type type)
		{
			var parameterLessConstructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (parameterLessConstructor != default)
			{
				return (RouteRecord)parameterLessConstructor.Invoke(Array.Empty<object>());
			}

			return default;
		}
	}
}
