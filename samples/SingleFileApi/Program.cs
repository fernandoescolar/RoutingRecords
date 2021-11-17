using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RoutingRecords;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouteRecords();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapRouteRecords();
                               

app.Run();

record Hello0()
	: Get("/0", (ctx) =>
	   ctx.Response.SendAsync("Welcome to one file RoutingRecords"));

record Hello1()
	: Get("/1", (req, res) =>
	   res.SendAsync("Welcome to one file RoutingRecords"));

record Hello2()
	: Get("/2/{id:int}", (int id) =>
		Send($"Welcome to: {id}"));