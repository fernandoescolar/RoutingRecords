using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace RoutingRecords.UnitTests
{
	public class FromQuery_Should
	{
		private readonly Dictionary<string, StringValues> _query;
		private readonly Mock<HttpRequest> _request;

		public FromQuery_Should()
		{
			_query = new Dictionary<string, StringValues>();
			_request = new Mock<HttpRequest>();
			_request.SetupGet(x => x.Query).Returns(() => new QueryCollection(_query));
		}

		[Fact]
		public void Convert_object_from_string()
		{
			_query["id"] = "1";

			var actual = _request.Object.FromQuery<int>("id");
			Assert.Equal(1, actual);
		}

		[Fact]
		public void Not_convert_When_is_the_expected_type()
		{
			var expected = "__test__";
			_query["id"] = expected;

			var actual = _request.Object.FromQuery<object>("id");
			Assert.Equal(expected.GetHashCode(), actual.GetHashCode());
		}

		[Fact]
		public void Out_return_default_When_it_is_null()
		{
			_query["id"] = new StringValues();

			var actual = _request.Object.FromQuery<int>("id");
			Assert.Equal(default, actual);
		}
	}
}
