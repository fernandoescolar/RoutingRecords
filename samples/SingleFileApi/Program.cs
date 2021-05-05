using RoutingRecords;

new ApiApp().Run();

record Hello()
	: Get("/", (req, res) =>
		res.SendAsync("Welcome to one file RoutingRecords"));