using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Builder
{
	internal class RecordEndpointConventionBuilderCollection : IRecordEndpointConventionBuilderCollection
	{
		private readonly List<IRecordEndpointConventionBuilder> _innerBuilders;

		public RecordEndpointConventionBuilderCollection(IEnumerable<IRecordEndpointConventionBuilder> innerBuilders)
		{
			_innerBuilders = new List<IRecordEndpointConventionBuilder>(innerBuilders);
		}

		public void Add(Action<EndpointBuilder> convention)
			=> _innerBuilders.ForEach(x => x.Add(convention));

		public IEnumerator<IRecordEndpointConventionBuilder> GetEnumerator()
			=> _innerBuilders.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> _innerBuilders.GetEnumerator();
	}
}
