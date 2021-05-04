using RoutingRecords.IntegrationTests.TestServer;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.IntegrationTests
{
	public class FromQueryTests
	{
		private readonly HttpClient _client;

		public FromQueryTests()
		{
			_client = new Server().CreateClient();
		}

		[Fact]
		public Task Alpha()
		{
			const string url = "/query-values/alpha?id=aaa";
			const string expected = "aaa";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Bool()
		{
			const string url = "/query-values/bool?id=TRUE";
			const string expected = "True";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task DateTime()
		{
			const string url = "/query-values/datetime?id=2000-01-01 01:02:03";
			const string expected = "2000-01-01T01:02:03";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Decimal()
		{
			const string url = "/query-values/decimal?id=1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Double()
		{
			const string url = "/query-values/double?id=1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Float()
		{
			const string url = "/query-values/float?id=1.23";
			const string expected = "1.23";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Guid()
		{
			const string url = "/query-values/guid?id=9C266138-C124-4972-87E0-124F32811923";
			const string expected = "9c266138-c124-4972-87e0-124f32811923";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Int()
		{
			const string url = "/query-values/int?id=123";
			const string expected = "123";

			return TestRoute(url, expected);
		}

		[Fact]
		public Task Long()
		{
			const string url = "/query-values/long?id=9876543210";
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
