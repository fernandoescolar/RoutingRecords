using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Threading.Tasks;

namespace RoutingRecords
{
	public interface IResponse
	{
		IResponse Status(int statusCode);

		IResponse Status(HttpStatusCode statusCode);

		IResponse Send(string body);

		IResponse Send(string body, string mediaType);

		IResponse SendFile(IFileInfo body);

		IResponse SendFile(string filename);

		IResponse Json<T>(T body);

		Task ExecuteAsync(HttpResponse res);
	}
}
