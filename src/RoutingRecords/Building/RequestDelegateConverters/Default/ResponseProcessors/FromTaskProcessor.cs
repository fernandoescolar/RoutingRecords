namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors;

public class FromTaskProcessor : ResponseProcessor<Task>
{
    protected override Task OnProcessAsync(HttpContext context, Task result)
        => result;
}
