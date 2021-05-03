using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using RoutingRecords.IntegrationTests.TestServer;
using Xunit;

namespace RoutingRecords.IntegrationTests
{
    public class ToJsonTests
    {
        private readonly HttpClient _client;

        public ToJsonTests()
        {
            _client = new Server().CreateClient();
        }

        [Fact]
        public async Task GetJson()
        {
            const string url = "/json";
            const string expected = @"{""id"":1,""name"":""test"",""active"":true}";

            var res = await _client.GetAsync(url);
            var actual = await res.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetJsonMimeType()
        {
            const string url = "/json";
            const string expected = MediaTypeNames.Application.Json;

            var res = await _client.GetAsync(url);
            var actual = res.Content.Headers.ContentType.MediaType;

            Assert.Equal(expected, actual);
        }
    }
}
