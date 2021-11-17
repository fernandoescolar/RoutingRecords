namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors;

public class EmptyResponseProcessor : IResponseProcessor
{
    public bool CanProcess(Type resultType)
        => true;

    public Task ProcessAsync(HttpContext context, object result)
        => Task.CompletedTask;
}
