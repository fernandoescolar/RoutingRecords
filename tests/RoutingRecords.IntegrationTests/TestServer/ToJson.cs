namespace RoutingRecords.IntegrationTests.TestServer
{
    public record Output(int Id, string Name, bool Active);

    public record ToJson()
		: Get("json", async (req, res) =>
        {
            await res.JsonAsync(new Output(1, "test", true));
        });
}