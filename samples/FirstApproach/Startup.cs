using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstApproach
{
	public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TodoStore>();
            services.AddRouteRecords();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoStore store)
        {
            // seed data
            for (var i = 0; i < 100; i++) store.Insert(new Todo(default, $"demo task {i}", false));
            ////

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
        }
    }
}
