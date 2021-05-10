using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class FromQueryBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.GetCustomAttribute<FromQueryAttribute>() != default;

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
		{
			var name = parameterInfo.Name;
			var type = parameterInfo.ParameterType;
			return ctx => ctx.Request.FromQuery(type, name);
		}
	}
}
