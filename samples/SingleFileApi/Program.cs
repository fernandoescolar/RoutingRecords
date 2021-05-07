using RoutingRecords;
using System;

new ApiApp().Run();

record Hello0()
	: Get("/0", (ctx) =>
	   ctx.Response.SendAsync("Welcome to one file RoutingRecords"));

record Hello1()
	: Get("/1", (req, res) =>
	   res.SendAsync("Welcome to one file RoutingRecords"));

record Hello2()
	: Get("/2/{id:int}", new Func<int, IResponse>((id) =>
		Send($"Welcome to: {id}")));