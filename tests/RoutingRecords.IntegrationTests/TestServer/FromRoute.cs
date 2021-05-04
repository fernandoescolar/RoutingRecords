
using System;

namespace RoutingRecords.IntegrationTests.TestServer
{
	/***************
    Reference:
    https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-5.0#route-constraint-reference
    */

	public record GetRouteAlpha()
		: Get("route-values/alpha/{id:alpha}", async (req, res) =>
		{
			var a = req.FromRoute<string>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteBool()
		: Get("route-values/bool/{id:bool}", async (req, res) =>
		{
			var a = req.FromRoute<bool>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteDateTime()
		: Get("route-values/datetime/{id:datetime}", async (req, res) =>
		{
			var a = req.FromRoute<DateTime>("id");
			await res.SendAsync(a.ToString("yyyy-MM-ddTHH:mm:ss"));
		});

	public record GetRouteDecimal()
		: Get("route-values/decimal/{id:decimal}", async (req, res) =>
		{
			var a = req.FromRoute<decimal>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteDouble()
		: Get("route-values/double/{id:double}", async (req, res) =>
		{
			var a = req.FromRoute<double>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteFloat()
		: Get("route-values/float/{id:float}", async (req, res) =>
		{
			var a = req.FromRoute<float>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteGuid()
		: Get("route-values/guid/{id:guid}", async (req, res) =>
		{
			var a = req.FromRoute<Guid>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteInt()
		: Get("route-values/int/{id:int}", async (req, res) =>
		{
			var a = req.FromRoute<int>("id");
			await res.SendAsync(a.ToString());
		});

	public record GetRouteLong()
		: Get("route-values/long/{id:long}", async (req, res) =>
		{
			var a = req.FromRoute<long>("id");
			await res.SendAsync(a.ToString());
		});
}
