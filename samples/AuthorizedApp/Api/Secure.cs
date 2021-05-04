using RoutingRecords;

namespace AuthorizedApp.Api
{
	public record Secure()
		: Get("/secure", (req, res) =>
			res.SendAsync("If you can see this, you are authorized"));
}