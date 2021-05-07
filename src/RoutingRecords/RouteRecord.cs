using Microsoft.Extensions.FileProviders;
using System;
using System.Net;

namespace RoutingRecords
{
	/// <summary>
	/// Represents an instance <see cref="Microsoft.AspNetCore.Routing.RouteEndpoint"/> using <see cref="record"/> types
	/// that matches HTTP requests for the specified HTTP methods and pattern.
	/// </summary>
	/// <param name="Pattern"> A string representation of the route pattern.</param>
	/// <param name="Verb">The HTTP method that the endpoint will match.</param>
	/// <param name="Delegate">The delegate executed when the endpoint is matched.</param>
	public abstract record RouteRecord(string Pattern, string Verb, Delegate Delegate) 
	{
		public static IResponse Json<T>(T body)
			=> new ResponseBuilder().Json(body);

		public static IResponse Send(string body)
			=> new ResponseBuilder().Send(body);

		public static IResponse Send(string body, string mediaType)
			=> new ResponseBuilder().Send(body, mediaType);

		public static IResponse SendFile(IFileInfo body)
			=> new ResponseBuilder().SendFile(body);

		public static IResponse SendFile(string filename)
			=> new ResponseBuilder().SendFile(filename);

		public static IResponse Status(int statusCode)
			=> new ResponseBuilder().Status(statusCode);

		public static IResponse Status(HttpStatusCode statusCode)
			=> new ResponseBuilder().Status(statusCode);
	}
}
