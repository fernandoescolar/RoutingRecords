namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors;

public class FromTaskWithResultProcessor : ResponseProcessor<Task<IResult>>
{
    protected override async Task OnProcessAsync(HttpContext context, Task<IResult> result)
    {
        var response = await result;
        await response.ExecuteAsync(context);
    }
}
