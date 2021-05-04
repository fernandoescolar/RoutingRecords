using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace RoutingRecords.UnitTests.HttpRequestExtensions
{
	public class TryFromQuery_Should
	{
		private readonly Dictionary<string, StringValues> _query;
		private readonly Mock<HttpRequest> _request;

		public TryFromQuery_Should()
		{
			_query = new Dictionary<string, StringValues>();
			_request = new Mock<HttpRequest>();
			_request.SetupGet(x => x.Query).Returns(() => new QueryCollection(_query));
		}

		[Fact]
		public void Return_false_When_not_exists()
		{
			var actual = _request.Object.TryFromQuery<int>("id", out _);
			Assert.False(actual);
		}

		[Fact]
		public void Return_true_When_exists()
		{
			_query["id"] = string.Empty;

			var actual = _request.Object.TryFromQuery<int>("id", out _);
			Assert.True(actual);
		}

		[Fact]
		public void Convert_object_from_string()
		{
			_query["id"] = "1";

			_request.Object.TryFromQuery<int>("id", out var actual);
			Assert.Equal(1, actual);
		}

		[Fact]
		public void Not_convert_When_is_the_expected_type()
		{
			var expected = "__test__";
			_query["id"] = expected;

			_request.Object.TryFromQuery<object>("id", out var actual);
			Assert.Equal(expected.GetHashCode(), actual.GetHashCode());
		}

		[Fact]
		public void Out_return_default_When_it_is_null()
		{
			_query["id"] = new StringValues();

			_request.Object.TryFromQuery<int>("id", out var actual);
			Assert.Equal(default, actual);
		}
	}
}
