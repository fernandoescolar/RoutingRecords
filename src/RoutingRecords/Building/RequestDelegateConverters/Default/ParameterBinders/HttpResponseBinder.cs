using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class HttpResponseBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.ParameterType.Is<HttpResponse>();

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
			=> ctx => ctx.Response;
	}
}
