using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace RoutingRecords.UnitTests
{
    public class TryFromRoute_Should
    {
        private readonly RouteValueDictionary _routeValues;
        private readonly Mock<HttpRequest> _request;

        public TryFromRoute_Should()
        {
            _routeValues = new RouteValueDictionary();
            _request = new Mock<HttpRequest>();
            _request.SetupGet(x => x.RouteValues).Returns(_routeValues);
        }

        [Fact]
        public void Return_false_When_not_exists()
        {
            var actual = _request.Object.TryFromRoute<int>("id", out _);
            Assert.False(actual);
        }

        [Fact]
        public void Return_true_When_exists()
        {
            _routeValues["id"] = string.Empty;

            var actual = _request.Object.TryFromRoute<int>("id", out _);
            Assert.True(actual);
        }

        [Fact]
        public void Convert_object_from_string()
        {
            _routeValues["id"] = "1";

            _request.Object.TryFromRoute<int>("id", out var actual);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Not_convert_When_is_the_expected_type()
        {
            var expected =  new object();
            _routeValues["id"] = expected;

            _request.Object.TryFromRoute<object>("id", out var actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Out_return_default_When_it_is_null()
        {
            _routeValues["id"] = null;

            _request.Object.TryFromRoute<int>("id", out var actual);
            Assert.Equal(default, actual);
        }
    }
}
