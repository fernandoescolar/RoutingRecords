using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecords.IntegrationTests.TestServer
{
	public record Input(int Id, string Name, bool Active);

	public record FromJson()
		: Post("json", async (req, res) =>
		{
			var a = await req.FromJsonAsync<Input>();
			if (a == null)
			{
				res.Status(Status400BadRequest);
			}
			else
			{
				res.Status(Status202Accepted);
			}
		});
}