using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingRecords
{
	public static class EndpointRouteBuilderExtensions
	{
		/// <summary>
		/// Adds every <see cref="RouteRecord"/> endpoints to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
		/// </summary>
		/// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
		/// <returns>A <see cref="IEndpointRouteBuilder" /> that can be used to further customize the endpoint.</returns>
		public static IEndpointRouteBuilder MapRouteRecords(this IEndpointRouteBuilder endpoints)
		{
			using var scope = endpoints.ServiceProvider.CreateScope();
			scope.ServiceProvider
				 .GetServices<RouteRecord>()
				 .ToList()
				 .ForEach(handler => endpoints.MapRouteRecord(handler));

			return endpoints;
		}

		private static IEndpointConventionBuilder MapRouteRecord(this IEndpointRouteBuilder endpoints, RouteRecord route)
		{
			var type = route.GetType();
			return endpoints.MapMethod(route.Pattern,
									   route.Verb,
									   ctx => RouteRecordHandler(ctx, type));
		}

		private static IEndpointConventionBuilder MapMethod(this IEndpointRouteBuilder endpoints, string pattern, string verb, RequestDelegate requestDelegate)
		{
			return endpoints.MapMethods(pattern, new[] { verb }, requestDelegate);
		}

		private static Task RouteRecordHandler(HttpContext ctx, Type type)
		{
			var r = (RouteRecord)ctx.RequestServices.GetService(type);
			return r.RouteDelegate(ctx.Request, ctx.Response);
		}
	}
}
