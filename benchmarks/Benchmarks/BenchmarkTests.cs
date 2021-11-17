namespace Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class BenchmarkTests
{
    private readonly Random _rnd = new Random();
    private HttpClient _mvcClient;
    private HttpClient _routesClient;

    private HttpClient _bindingClient;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var mvcFactory = new WebApplicationFactory<MvcApp.Startup>();
        _mvcClient = mvcFactory.CreateClient();

        var routesFactory = new WebApplicationFactory<RoutingRecordsApp.Startup>();
        _routesClient = routesFactory.CreateClient();

        var bindingFactory = new WebApplicationFactory<RoutingRecordsAutoBindingApp.Startup>();
        _bindingClient = bindingFactory.CreateClient();
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Add")]
    public Task Add_Mvc()
    {
        return _mvcClient.PostAsync("/todos", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Get")]
    public Task Get_Mvc()
    {
        return _mvcClient.GetAsync("/todos");
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Update")]
    public Task Update_Mvc()
    {
        return _mvcClient.PutAsync("/todos/10", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Benchmark]
    [BenchmarkCategory("Add")]
    public Task Add_Routes()
    {
        return _routesClient.PostAsync("/todos", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Benchmark]
    [BenchmarkCategory("Get")]
    public Task Get_Routes()
    {
        return _routesClient.GetAsync("/todos");
    }

    [Benchmark]
    [BenchmarkCategory("Update")]
    public Task Update_Routes()
    {
        return _routesClient.PutAsync("/todos/10", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Benchmark]
    [BenchmarkCategory("Add")]
    public Task Add_Routes_Binding()
    {
        return _bindingClient.PostAsync("/todos", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Benchmark]
    [BenchmarkCategory("Get")]
    public Task Get_Routes_Binding()
    {
        return _bindingClient.GetAsync("/todos");
    }

    [Benchmark]
    [BenchmarkCategory("Update")]
    public Task Update_Routes_Binding()
    {
        return _bindingClient.PutAsync("/todos/10", new StringContent($@"{{""title"":""Task {_rnd.NextDouble()}""}}", Encoding.UTF8, MediaTypeNames.Application.Json));
    }
}
