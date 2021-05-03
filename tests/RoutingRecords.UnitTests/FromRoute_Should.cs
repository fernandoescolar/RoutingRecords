using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace RoutingRecords.UnitTests
{
    public class FromRoute_Should
    {
        private readonly RouteValueDictionary _routeValues;
        private readonly Mock<HttpRequest> _request;

        public FromRoute_Should()
        {
            _routeValues = new RouteValueDictionary();
            _request = new Mock<HttpRequest>();
            _request.SetupGet(x => x.RouteValues).Returns(_routeValues);
        }

        [Fact]
        public void Convert_object_from_string()
        {
            _routeValues["id"] = "1";

            var actual = _request.Object.FromRoute<int>("id");
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Not_convert_When_is_the_expected_type()
        {
            var expected =  new object();
            _routeValues["id"] = expected;

            var actual = _request.Object.FromRoute<object>("id");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Out_return_default_When_it_is_null()
        {
            _routeValues["id"] = null;

            var actual = _request.Object.FromRoute<int>("id");
            Assert.Equal(default, actual);
        }
    }
}
