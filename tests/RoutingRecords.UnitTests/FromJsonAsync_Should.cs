using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RoutingRecords.UnitTests
{
    public class FromJsonAsync_Should
	{
		private readonly Mock<HttpRequest> _request;
		private readonly Mock<HttpContext> _context;
		private readonly Mock<IServiceProvider> _serviceProvider;
		private string _bodyText = "{}";
		private string _mediaType  = "application/json";
		private CancellationToken _requestAborted =  new CancellationToken();

		public FromJsonAsync_Should()
		{
			var lazyBody = new Lazy<Stream>(() => new MemoryStream(Encoding.UTF8.GetBytes(_bodyText)));

			_request = new Mock<HttpRequest>();
			_request.SetupGet(x => x.HttpContext)
			        .Returns(() => _context.Object);
			_request.SetupGet(x => x.ContentType)
			        .Returns(() => _mediaType);
			_request.SetupGet(x => x.Body)
			        .Returns(() => lazyBody.Value);

			_serviceProvider = new Mock<IServiceProvider>();
			_context = new Mock<HttpContext>();
			_context.SetupGet(x => x.RequestAborted)
			        .Returns(() => _requestAborted);
			_context.SetupGet(x => x.RequestServices)
			        .Returns(() => _serviceProvider.Object);
		}

		[Fact]
		public Task Throw_exception_When_request_have_been_aborted()
		{
			_requestAborted = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _request.Object.FromJsonAsync<object>());
		}

		[Fact]
		public Task Throw_exception_When_cancellation_token_has_been_cancelled()
		{
			var cancellationToken = new CancellationToken(true);

			return Assert.ThrowsAsync<OperationCanceledException>(() => _request.Object.FromJsonAsync<object>(cancellationToken: cancellationToken));
		}

		[Fact]
		public Task Throw_exception_When_validates_a_bad_media_type()
		{
			_mediaType = "text/plain";

			return Assert.ThrowsAsync<InvalidMediaTypeException>(() => _request.Object.FromJsonAsync<object>(validateMediaType: true));
		}

		[Fact]
		public async Task Log_warning_When_media_type_is_not_json()
		{
			const string expectedMesage = "Expected 'application/json' content-type. Found: 'text/plain'";
			var logger = new Mock<ILogger<HttpRequest>>();

			_mediaType = "text/plain";
			_serviceProvider.Setup(x => x.GetService(typeof(ILogger<HttpRequest>)))
			                .Returns(logger.Object);

			await _request.Object.FromJsonAsync<object>();

			logger.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(expectedMesage, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
		}

		[Fact]
		public async Task Log_error_When_it_can_not_deserialize_an_object()
		{
			const string expectedMesage = "Cannot deserialize Json:";
			var logger = new Mock<ILogger<HttpRequest>>();

			_bodyText = "anything";
			_serviceProvider.Setup(x => x.GetService(typeof(ILogger<HttpRequest>)))
			                .Returns(logger.Object);

			await _request.Object.FromJsonAsync<object>();

			logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().StartsWith(expectedMesage, StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
		}

		[Fact]
		public async Task Return_null_When_it_can_not_deserialize_an_object()
		{
			var logger = new Mock<ILogger<HttpRequest>>();

			_bodyText = "anything";
			_serviceProvider.Setup(x => x.GetService(typeof(ILogger<HttpRequest>)))
			                .Returns(logger.Object);

			var actual = await _request.Object.FromJsonAsync<object>();

			Assert.Null(actual);
		}

        private record Item(int Id);

		[Fact]
		public async Task Return_a_deserialized_object()
		{
			_bodyText = @"{""id"":1}";
			var actual = await _request.Object.FromJsonAsync<Item>();

			Assert.Equal(1, actual.Id);
		}
	}
}
