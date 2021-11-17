namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders;

public class CancellationTokenBinder : IParameterBinder
{
    public bool CanResolve(ParameterInfo parameterInfo)
        => parameterInfo.ParameterType.Is<CancellationToken>();

    public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
        => ctx => ctx.RequestAborted;
}
