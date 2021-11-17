namespace RoutingRecords.Building.RequestDelegateConverters.Default.ParameterBinders;

public class ClaimsPrincipalTokenBinder : IParameterBinder
{
    public bool CanResolve(ParameterInfo parameterInfo)
        => parameterInfo.ParameterType.Is<System.Security.Claims.ClaimsPrincipal>();

    public ParameterBinding CreateBinding(ParameterInfo parameterInfo)
        => ctx => ctx.User;
}
