namespace SampleApp.Api;

public record Hello()
    : Get("/", (req, res) =>
        res.SendAsync("<h1>Welcome to RoutingRecords</h1><p>You can go to <a href=\"/swagger/index.html\">Swagger UI</a> to explore the API.</p>", "text/html"));
