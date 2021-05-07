using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
	public class FromRouteBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> parameterInfo.GetCustomAttribute<FromRouteAttribute>() != default;

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
		{
			var name = parameterInfo.Name;
			var type = parameterInfo.ParameterType;
			return ctx => ctx.Request.FromRoute(type, name).AsTask();
		}
	}
}
