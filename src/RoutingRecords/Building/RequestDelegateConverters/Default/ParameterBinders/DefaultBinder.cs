using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders
{
    public class DefaultBinder : IParameterBinder
	{
		public bool CanResolve(ParameterInfo parameterInfo)
			=> true;

		public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
			=> ctx => Find(ctx, parameterInfo.ParameterType, parameterInfo.Name);

		private static object Find(HttpContext ctx, Type type, string name)
			=> type.IsClass && type.IsNot<string>() ?
			   ctx.Request.FromJsonAsync(type)
			 : FindParameter(ctx, type, name);

		private static object FindParameter(HttpContext ctx, Type type, string name)
			=> ctx.Request.TryFromRoute(type, name, out object r) ? r
			 : ctx.Request.TryFromHeader(type, name, out var h) ? h
			 : ctx.Request.TryFromQuery(type, name, out var q) ? q
			 : default;
	}
}
