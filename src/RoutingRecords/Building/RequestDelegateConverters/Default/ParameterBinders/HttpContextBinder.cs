using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class HttpContextBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.ParameterType.Is<HttpContext>();

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
			=> ctx => ctx;
	}
}
