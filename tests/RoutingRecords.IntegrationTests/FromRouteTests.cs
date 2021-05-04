using RoutingRecords.IntegrationTests.TestServer;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.IntegrationTests
{
	public class FromRouteTests
	{
		private readonly HttpClient _client;

		public FromRouteTests()
		{
			_client = new Server().CreateClient();
		}

		[Fact]
		public Task Alpha()
		{
			const string url = "/route-values/alpha/aaa";
			const string expected = "aaa";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Bool()
		{
			const string url = "/route-values/bool/TRUE";
			const string expected = "True";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task DateTime()
		{
			const string url = "/route-values/datetime/2000-01-01 01:02:03";
			const string expected = "2000-01-01T01:02:03";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Decimal()
		{
			const string url = "/route-values/decimal/1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Double()
		{
			const string url = "/route-values/double/1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Float()
		{
			const string url = "/route-values/float/1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Guid()
		{
			const string url = "/route-values/guid/9C266138-C124-4972-87E0-124F32811923";
			const string expected = "9c266138-c124-4972-87e0-124f32811923";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Int()
		{
			const string url = "/route-values/int/123";
			const string expected = "123";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Long()
		{
			const string url = "/route-values/long/9876543210";
			const string expected = "9876543210";

			return TestRoute(url, expected);
		}

		private async Task TestRoute(string url, string expected)
		{
			var res = await _client.GetAsync(url);
			var actual = await res.Content.ReadAsStringAsync();
			Assert.Equal(expected, actual);
		}
	}
}
