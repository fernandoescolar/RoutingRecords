using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors
{
	public class FromTaskWithResponseProcessor : ResponseProcessor<Task<IResponse>>
	{
		protected override async Task OnProcessAsync(HttpContext context, Task<IResponse> result)
		{
			var response = await result;
			await response.ExecuteAsync(context.Response);
		}
	}
}
