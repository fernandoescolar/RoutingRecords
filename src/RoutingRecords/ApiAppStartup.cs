using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RoutingRecords
{
    public class ApiAppStartup
    {
        private Action<IServiceCollection> _configureServices;
        private Action<IApplicationBuilder, IWebHostEnvironment> _configure;

        public ApiAppStartup(Action<IServiceCollection> configureServices, Action<IApplicationBuilder, IWebHostEnvironment> configure)
        {
            _configureServices = configureServices;
            _configure = configure;
        }

        public void ConfigureServices(IServiceCollection services)
		{
            _configureServices?.Invoke(services);
			services.AddRouteRecords();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

            _configure?.Invoke(app, env);

			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
    }
}