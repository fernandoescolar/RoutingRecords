using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors
{
	public class FromResponseProcessor : ResponseProcessor<IResponse>
	{
		protected override Task OnProcessAsync(HttpContext context, IResponse result)
			=> result.ExecuteAsync(context.Response);
	}
}
