using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.UnitTests.HttpResponseExtensions
{
    public class JsonAsync_Should
	{
		private readonly Mock<HttpResponse> _response;
        private readonly Mock<HttpContext> _context;
		private readonly MemoryStream _body;
        private string _mediaType;
        private CancellationToken _requestAborted =  new CancellationToken();

		public JsonAsync_Should()
		{
            _body = new MemoryStream();

            _context = new Mock<HttpContext>();
			_context.SetupGet(x => x.RequestAborted)
			        .Returns(() => _requestAborted);

			_response = new Mock<HttpResponse>();
            _response.SetupGet(x => x.HttpContext)
			         .Returns(() => _context.Object);
			_response.SetupGet(x => x.Body)
			         .Returns(() => _body);
            _response.SetupSet(x => x.ContentType = It.IsAny<string>())
			         .Callback<string>(s => _mediaType = s);
		}

		[Fact]
		public async Task Set_media_type_as_application_json()
		{
            const string expected = "application/json";
			var any_object = new object();

			await _response.Object.JsonAsync(any_object);

			Assert.Equal(expected, _mediaType);
		}


		[Fact]
		public async Task Set_object_serialized_as_json_in_response_body()
        {
            const string expected = @"{""id"":1}";
			var specific_object = new { Id = 1 };

            await _response.Object.JsonAsync(specific_object);

            var actual = GetBodyText();
            Assert.Equal(expected, actual);
        }

        [Fact]
		public Task Throw_exception_When_request_have_been_aborted()
		{
			var any_object = new object();

			_requestAborted = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _response.Object.JsonAsync(any_object));
		}

		[Fact]
		public Task Throw_exception_When_cancellation_token_has_been_cancelled()
		{
			var any_object = new object();
			var cancellationToken = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _response.Object.JsonAsync(any_object, cancellationToken: cancellationToken));
		}

        private string GetBodyText()
        {
            _body.Position = 0;
            using var reader = new StreamReader(_body);
            return reader.ReadToEnd();
        }
	}
}
