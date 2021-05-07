using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RoutingRecords
{
	internal class ResponseBuilder : IResponse
	{
		private int? _statusCode = null;
		private string _mediaType = null;
		private Func<HttpResponse, Task> _bodyWriter = r => Task.CompletedTask;

		public Task ExecuteAsync(HttpResponse res)
		{
			if (!string.IsNullOrWhiteSpace(_mediaType))
			{
				res.ContentType = _mediaType;
			}

			if (_statusCode.HasValue)
			{
				res.StatusCode = _statusCode.Value;
			}

			return _bodyWriter(res);
		}

		public IResponse Json<T>(T body)
		{
			_bodyWriter = res => res.JsonAsync(body);
			return this;
		}

		public IResponse Send(string body)
		{
			_bodyWriter = res => res.SendAsync(body);
			return this;
		}

		public IResponse Send(string body, string mediaType)
		{
			_mediaType = mediaType;
			_bodyWriter = res => res.SendAsync(body);
			return this;
		}

		public IResponse SendFile(IFileInfo fileInfo)
		{
			_bodyWriter = res => res.SendFileAsync(fileInfo);
			return this;
		}

		public IResponse SendFile(string filename)
		{
			_bodyWriter = res => res.SendFileAsync(filename);
			return this;
		}

		public IResponse Status(int statusCode)
		{
			_statusCode = statusCode;
			return this;
		}

		public IResponse Status(HttpStatusCode statusCode)
		{
			_statusCode = (int)statusCode;
			return this;
		}
	}
}
