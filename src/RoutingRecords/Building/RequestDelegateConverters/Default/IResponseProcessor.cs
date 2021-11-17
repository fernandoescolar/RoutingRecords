namespace RoutingRecords.Building.RequestDelegateConverters.Default;

public interface IResponseProcessor
{
    bool CanProcess(Type resultType);

    Task ProcessAsync(HttpContext context, object result);
}
