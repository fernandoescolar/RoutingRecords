namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders;

public class FromBodyBinder : IParameterBinder
{
    public bool CanResolve(ParameterInfo parameterInfo)
        => parameterInfo.GetCustomAttribute<FromBodyAttribute>() != default;

    public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
    {
        var type = parameterInfo.ParameterType;
        return ctx => ctx.Request.FromJsonAsync(type);
    }
}
