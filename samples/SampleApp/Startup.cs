using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutingRecords;

namespace SampleApp
{
    public class Startup
    {
       public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<TodoStore>();
			services.AddRoutes(typeof(Startup).Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
    }
}
