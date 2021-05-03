using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace RoutingRecords.IntegrationTests.TestServer
{
    public class Startup
    {
       public void ConfigureServices(IServiceCollection services)
		{
			services.AddRoutes(typeof(Startup).Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRequestLocalization(options => options.DefaultRequestCulture = new RequestCulture("en-us"));
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
    }
}
