namespace RoutingRecords.Building.RequestDelegateConverters;

public class FromDefaultDelegate : IRequestDelegateConverter
{
    private readonly IEnumerable<IParameterBinder> _parameterBinders;
    private readonly IEnumerable<IResponseProcessor> _processors;

    public FromDefaultDelegate(IEnumerable<IParameterBinder> parameterBinders, IEnumerable<IResponseProcessor> processors)
    {
        _parameterBinders = parameterBinders;
        _processors = processors;
    }

    public bool CanConvert(Delegate @delegate)
        => true;

    public RequestDelegate Convert(Type routerecordType, Delegate @delegate)
    {
        var methodInfo = @delegate.Method;
        var args = GetParametersResolvers(methodInfo).ToList();
        var resultProcessor = GetResultProcessor(methodInfo);

        return async ctx =>
        {
            var parameters = new object[args.Count];
            for (var i = 0; i < args.Count; i++)
            {
                var p = args[i](ctx);
                if (p is Task<object> t)
                {
                    p = await t;
                }

                parameters[i] = p;
            }

            var r = (RouteRecord)ctx.RequestServices.GetService(routerecordType);
            var result = r.Delegate.DynamicInvoke(parameters);
            await resultProcessor.ProcessAsync(ctx, result);
        };
    }

    private IEnumerable<ParameterBinding> GetParametersResolvers(MethodInfo methodInfo)
    {
        foreach (var parameterInfo in methodInfo.GetParameters())
        {
            var binder = _parameterBinders.First(x => x.CanResolve(parameterInfo));
            yield return binder.CreateBinding(parameterInfo);
        }
    }

    private IResponseProcessor GetResultProcessor(MethodInfo methodInfo)
        => _processors.First(x => x.CanProcess(methodInfo.ReturnType));
}
