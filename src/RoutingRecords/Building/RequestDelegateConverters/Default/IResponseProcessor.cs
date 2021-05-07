using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RoutingRecords.Building.RequestDelegateConverters.Default
{
	public interface IResponseProcessor
	{
		bool CanProcess(Type resultType);

		Task ProcessAsync(HttpContext context, object result);
	}
}
