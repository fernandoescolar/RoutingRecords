namespace RoutingRecords.Building.RequestDelegateConverters.Default;

public delegate object ParameterBinding(HttpContext ctx);

public interface IParameterBinder
{
    bool CanResolve(ParameterInfo parameterInfo);
    ParameterBinding CreateBinding(ParameterInfo parameterInfo);
}
