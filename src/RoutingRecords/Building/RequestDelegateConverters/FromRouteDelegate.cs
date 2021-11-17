namespace RoutingRecords.Building.RequestDelegateConverters;

public class FromRouteDelegate : IRequestDelegateConverter
{
    public bool CanConvert(Delegate @delegate)
        => @delegate is RouteDelegate;

    public RequestDelegate Convert(Type routerecordType, Delegate @delegate)
    {
        return ctx =>
        {
            var r = (RouteRecord)ctx.RequestServices.GetService(routerecordType);
            return ((RouteDelegate)r.Delegate)(ctx.Request, ctx.Response);
        };
    }
}
