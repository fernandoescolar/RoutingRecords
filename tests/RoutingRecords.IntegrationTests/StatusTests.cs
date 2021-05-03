using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RoutingRecords.IntegrationTests.TestServer;
using Xunit;

namespace RoutingRecords.IntegrationTests
{
    public class StatusTests
    {
        private readonly HttpClient _client;

        public StatusTests()
        {
            _client = new Server().CreateClient();
        }

        [Fact]
        public Task Default()
        {
            const string url = "/status/default";
            const HttpStatusCode expected = HttpStatusCode.OK;

           return TestStatus(url, expected);
        }

        [Fact]
        public Task Number408()
        {
            const string url = "/status/number/408";
            const HttpStatusCode expected = HttpStatusCode.RequestTimeout;

           return TestStatus(url, expected);
        }

        [Fact]
        public Task TypeBadRequest()
        {
            const string url = "/status/type/badrequest";
            const HttpStatusCode expected = HttpStatusCode.BadRequest;

           return TestStatus(url, expected);
        }

        private async Task TestStatus(string url, HttpStatusCode expected)
        {
            var res = await _client.GetAsync(url);
            var actual = res.StatusCode;
            Assert.Equal(expected, actual);
        }
    }
}
