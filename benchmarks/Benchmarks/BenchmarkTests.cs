using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
	[MemoryDiagnoser]
	[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
	[CategoriesColumn]
	public class BenchmarkTests
	{
		private readonly Random _rnd = new Random();
		private HttpClient _mvcClient;
		private HttpClient _routesClient;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var mvcFactory = new WebApplicationFactory<MvcApp.Startup>();
			_mvcClient = mvcFactory.CreateClient();

			var routesFactory = new WebApplicationFactory<RoutingRecordsApp.Startup>();
			_routesClient = routesFactory.CreateClient();
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
	}
}
