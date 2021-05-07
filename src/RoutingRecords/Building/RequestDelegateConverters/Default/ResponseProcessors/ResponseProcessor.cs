using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors
{
	public abstract class ResponseProcessor : IResponseProcessor
	{
		private readonly Type _type;

		public ResponseProcessor(Type type)
		{
			_type = type;
		}

		public bool CanProcess(Type resultType)
			=> resultType.Is(_type);

		public Task ProcessAsync(HttpContext context, object result)
			=> OnProcessAsync(context, result);

		protected abstract Task OnProcessAsync(HttpContext context, object result);
	}

	public abstract class ResponseProcessor<T> : ResponseProcessor
	{
		public ResponseProcessor() : base(typeof(T))
		{
		}

		protected override Task OnProcessAsync(HttpContext context, object result)
			=> OnProcessAsync(context, (T)result);

		protected abstract Task OnProcessAsync(HttpContext context, T result);
	}
}
