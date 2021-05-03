using RoutingRecords;

namespace SampleApp.Api
{
    public record Hello()
        : Get("/", (req, res) =>
            res.SendAsync("Welcome to RoutingRecords"));
}