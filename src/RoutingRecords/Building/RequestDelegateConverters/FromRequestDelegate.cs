namespace RoutingRecords.Building.RequestDelegateConverters;

public class FromRequestDelegate : IRequestDelegateConverter
{
    public bool CanConvert(Delegate @delegate)
        => @delegate is RequestDelegate;

    public RequestDelegate Convert(Type routerecordType, Delegate @delegate)
    {
        return ctx =>
        {
            var r = (RouteRecord)ctx.RequestServices.GetService(routerecordType);
            return ((RequestDelegate)r.Delegate)(ctx);
        };
    }
}
