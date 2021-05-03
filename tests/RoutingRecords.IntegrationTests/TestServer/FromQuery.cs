using System;

namespace RoutingRecords.IntegrationTests.TestServer
{
    public record GetQueryAlpha()
		: Get("query-values/alpha", async (req, res) =>
        {
            var a = req.FromQuery<string>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryBool()
		: Get("query-values/bool", async (req, res) =>
        {
            var a = req.FromQuery<bool>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryDateTime()
		: Get("query-values/datetime", async (req, res) =>
        {
            var a = req.FromQuery<DateTime>("id");
            await res.SendAsync(a.ToString("yyyy-MM-ddTHH:mm:ss"));
        });

    public record GetQueryDecimal()
		: Get("query-values/decimal", async (req, res) =>
        {
            var a = req.FromQuery<decimal>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryDouble()
		: Get("query-values/double", async (req, res) =>
        {
            var a = req.FromQuery<double>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryFloat()
		: Get("query-values/float", async (req, res) =>
        {
            var a = req.FromQuery<float>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryGuid()
		: Get("query-values/guid", async (req, res) =>
        {
            var a = req.FromQuery<Guid>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryInt()
		: Get("query-values/int", async (req, res) =>
        {
            var a = req.FromQuery<int>("id");
            await res.SendAsync(a.ToString());
        });

    public record GetQueryLong()
		: Get("query-values/long", async (req, res) =>
        {
            var a = req.FromQuery<long>("id");
            await res.SendAsync(a.ToString());
        });
}