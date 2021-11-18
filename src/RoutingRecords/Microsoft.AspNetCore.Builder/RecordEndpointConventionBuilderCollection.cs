namespace Microsoft.AspNetCore.Builder;

internal class RecordEndpointConventionBuilderCollection : IRecordEndpointConventionBuilderCollection
{
    private readonly List<IEndpointConventionBuilder> _innerBuilders;

    public RecordEndpointConventionBuilderCollection(IEnumerable<IEndpointConventionBuilder> innerBuilders)
    {
        _innerBuilders = new List<IEndpointConventionBuilder>(innerBuilders);
    }

    public void Add(Action<EndpointBuilder> convention)
        => _innerBuilders.ForEach(x => x.Add(convention));

    public IEnumerator<IEndpointConventionBuilder> GetEnumerator()
        => _innerBuilders.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _innerBuilders.GetEnumerator();
}
