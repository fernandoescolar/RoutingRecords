using RoutingRecords.IntegrationTests.TestServer;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.IntegrationTests
{
	public class FromJsonTests
	{
		private readonly HttpClient _client;

		public FromJsonTests()
		{
			_client = new Server().CreateClient();
		}

		[Fact]
		public Task SendInput()
		{
			const string url = "/json";
			const string data = @"{""id"":1,""name"":""test"",""active"":true}";
			const HttpStatusCode expected = HttpStatusCode.Accepted;

			return TestJson(url, data, expected);
		}

		[Fact]
		public Task DoNotSendInput()
		{
			const string url = "/json";
			const HttpStatusCode expected = HttpStatusCode.BadRequest;

			return TestJson(url, string.Empty, expected);
		}

		private async Task TestJson(string url, string data, HttpStatusCode expected)
		{
			var res = await _client.PostAsync(url, new StringContent(data));
			var actual = res.StatusCode;
			Assert.Equal(expected, actual);
		}
	}
}
