using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutingRecordsApp.Data;

namespace RoutingRecordsApp
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<TodoStore>();
			services.AddRouteRecords(typeof(Startup).Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoStore store)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// seed data
			for (var i = 0; i < 100; i++) store.Insert(new Todo(default, $"demo task {i}", false));
			////

			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
	}
}
