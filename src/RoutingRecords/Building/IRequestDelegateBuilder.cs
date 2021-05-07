using Microsoft.AspNetCore.Http;

namespace RoutingRecords.Building
{
	public interface IRequestDelegateBuilder
	{
		RequestDelegate CreateFor(RouteRecord route);
	}
}