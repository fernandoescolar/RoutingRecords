using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class HttpRequestBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.ParameterType.Is<HttpRequest>();

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
			=> ctx => ctx.Request;
	}
}
