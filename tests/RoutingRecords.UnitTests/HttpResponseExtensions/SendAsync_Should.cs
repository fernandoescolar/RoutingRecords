using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.UnitTests.HttpResponseExtensions
{
    public class SendAsync_Should
	{
		private readonly Mock<HttpResponse> _response;
        private readonly Mock<HttpContext> _context;
		private readonly MemoryStream _body;
        private string _mediaType;
        private CancellationToken _requestAborted =  new CancellationToken();

		public SendAsync_Should()
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
		public async Task Set_media_type_as_plain_text_by_default()
		{
            const string expected = "text/plain";
			await _response.Object.SendAsync("any text");

			Assert.Equal(expected, _mediaType);
		}

        [Fact]
		public async Task Set_specified_media_type()
		{
            const string expected = "some_media/type";
			await _response.Object.SendAsync("any text", expected);

			Assert.Equal(expected, _mediaType);
		}

		[Fact]
		public async Task Set_specified_text_in_response_body()
        {
            const string expected = "any text content";
            await _response.Object.SendAsync(expected);

            var actual = GetBodyText();
            Assert.Equal(expected, actual);
        }

        [Fact]
		public Task Throw_exception_When_request_have_been_aborted()
		{
			_requestAborted = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _response.Object.SendAsync("any text"));
		}

		[Fact]
		public Task Throw_exception_When_cancellation_token_has_been_cancelled()
		{
			var cancellationToken = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _response.Object.SendAsync("any text", cancellationToken: cancellationToken));
		}

        private string GetBodyText()
        {
            _body.Position = 0;
            using var reader = new StreamReader(_body);
            return reader.ReadToEnd();
        }
	}
}
