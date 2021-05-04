using System;

namespace Microsoft.AspNetCore.Builder
{
	internal class RecordEndpointConventionBuilder : IRecordEndpointConventionBuilder
	{
		private readonly IEndpointConventionBuilder _innerBuilder;

		public RecordEndpointConventionBuilder(Type routeRecordType, IEndpointConventionBuilder innerBuilder)
		{
			RouteRecordType = routeRecordType;
			_innerBuilder = innerBuilder;
		}

		public Type RouteRecordType { get; private set; }

		public void Add(Action<EndpointBuilder> convention)
		{
			_innerBuilder.Add(convention);
		}
	}
}
