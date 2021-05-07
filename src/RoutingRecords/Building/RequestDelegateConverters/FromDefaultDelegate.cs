using Microsoft.AspNetCore.Http;
using RoutingRecords.Building.RequestDelegateConverters.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RoutingRecords.Building.RequestDelegateConverters
{
	public class FromDefaultDelegate : IRequestDelegateConverter
	{
		private readonly IEnumerable<IParameterBinder> _parameterBinders;
		private readonly IEnumerable<IResponseProcessor> _processors;

		public FromDefaultDelegate(IEnumerable<IParameterBinder> parameterBinders, IEnumerable<IResponseProcessor> processors)
		{
			_parameterBinders = parameterBinders;
			_processors = processors;
		}

		public bool CanConvert(Delegate @delegate)
			=> true;

		public RequestDelegate Convert(Type routerecordType, Delegate @delegate)
		{
			var methodInfo = @delegate.Method;
			var args = GetParametersResolvers(methodInfo).ToList();
			var resultProcessor = GetResultProcessor(methodInfo);

			return async ctx =>
			{
				var r = (RouteRecord)ctx.RequestServices.GetService(routerecordType);
				var tasks = args.Select(x => x(ctx)).ToList();
				await Task.WhenAll(tasks);
				
				var parameters = tasks.Select(x => x.Result).ToArray();
				var result = r.Delegate.DynamicInvoke(parameters);
				await resultProcessor.ProcessAsync(ctx, result);
			};
		}

		private IEnumerable<ParameterBinding> GetParametersResolvers(MethodInfo methodInfo)
		{
			foreach (var parameterInfo in methodInfo.GetParameters())
			{
				var binder = _parameterBinders.First(x => x.CanResolve(parameterInfo));
				yield return binder.CreateBinding(parameterInfo);
			}	
		}

		private IResponseProcessor GetResultProcessor(MethodInfo methodInfo)
			=> _processors.First(x => x.CanProcess(methodInfo.ReturnType));

	}
}
