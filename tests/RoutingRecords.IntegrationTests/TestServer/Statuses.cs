using System.Net;
using System.Threading.Tasks;

namespace RoutingRecords.IntegrationTests.TestServer
{
	public record Default()
		: Get("status/default", (req, res) =>
		{
			return Task.CompletedTask;
		});

	public record Number408()
		: Get("status/number/408", (req, res) =>
		{
			res.Status(408);
			return Task.CompletedTask;
		});

	public record TypeBadRequest()
		: Get("status/type/badrequest", (req, res) =>
		{
			res.Status(HttpStatusCode.BadRequest);
			return Task.CompletedTask;
		});
}
