using RoutingRecords;

namespace AuthorizedApp.Api
{
	public record Hello()
		: Get("/", (req, res) =>
			res.SendAsync("Welcome to RoutingRecords"));
}