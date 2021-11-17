namespace RoutingRecords.Building.RequestDelegateConverters.Default.ResponseProcessors;

public class FromResultProcessor : ResponseProcessor<IResult>
{
    protected override Task OnProcessAsync(HttpContext context, IResult result)
        => result.ExecuteAsync(context);
}
