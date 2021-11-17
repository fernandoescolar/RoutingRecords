namespace RoutingRecords.Building;

public class RequestDelegateBuilder : IRequestDelegateBuilder
{
    private readonly IEnumerable<IRequestDelegateConverter> _converters;

    public RequestDelegateBuilder(IEnumerable<IRequestDelegateConverter> converters)
    {
        _converters = converters;
    }

    public RequestDelegate CreateFor(RouteRecord route)
    {
        var converter = _converters.First(x => x.CanConvert(route.Delegate));
        return converter.Convert(route.GetType(), route.Delegate);
    }
}
