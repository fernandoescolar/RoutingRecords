using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class FromHeaderBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.GetCustomAttribute<FromHeaderAttribute>() != default;

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
		{
			var name = parameterInfo.Name;
			var type = parameterInfo.ParameterType;
			return ctx => ctx.Request.FromHeader(type, name);
		}
	}
}
