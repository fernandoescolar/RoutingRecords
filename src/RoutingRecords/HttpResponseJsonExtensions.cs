using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingRecords
{
    public static class HttpResponseJsonExtensions
	{
		private static readonly JsonSerializerOptions _jsonOptions
			= new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

		/// <summary>
		/// Writes an object of type <see cref="T"/> with the media type "application/json" serialized as json in the specified <see cref="HttpResponse"/> body.
		/// </summary>
		/// <typeparam name="T">The type of the input object.</typeparam>
		/// <param name="res">The <see cref="HttpResponse"/>.</param>
		/// <param name="body">The object to serialize and write in the <see cref="HttpResponse"/> body.</param>
		/// <param name="cancellationToken">A token that may be used to cancel the read operation.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		public static Task JsonAsync<T>(this HttpResponse res, T body, CancellationToken cancellationToken = default)
		{
			cancellationToken = cancellationToken == default ? res.HttpContext.RequestAborted : cancellationToken;
            cancellationToken.ThrowIfCancellationRequested();

			res.ContentType = MediaTypeNames.Application.Json;
			return JsonSerializer.SerializeAsync(res.Body, body, _jsonOptions, cancellationToken);
		}
	}
}
