using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
using Xunit;

namespace RoutingRecords.UnitTests.HttpResponseExtensions
{
    public class Status_Should
	{
		private readonly Mock<HttpResponse> _response;
		private int _statusCode = 0;

		public Status_Should()
		{
			_response = new Mock<HttpResponse>();
			_response.SetupSet(x => x.StatusCode = It.IsAny<int>())
			         .Callback<int>(n => _statusCode = n);
		}

		[Fact]
		public void Set_response_status_code_from_number()
		{
			_response.Object.Status(999);

			Assert.Equal(999, _statusCode);
		}

		[Fact]
		public void Set_response_status_code_from_HttpStatusCode()
		{
			_response.Object.Status(HttpStatusCode.UpgradeRequired);

			Assert.Equal(426, _statusCode);
		}
	}
}
