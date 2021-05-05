using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RoutingRecords
{
    public class ApiApp
	{
        private Action<IServiceCollection> _configureServices;
        private Action<IApplicationBuilder, IWebHostEnvironment> _configure;

        public ApiApp ConfigureServices(Action<IServiceCollection> configureServices)
        {
            _configureServices = configureServices;
            return this;
        }

        public ApiApp Configure(Action<IApplicationBuilder, IWebHostEnvironment> configure)
        {
            _configure = configure;
            return this;
        }

        public void Run(params string[] args)
            => BuildHost(args).Run();

        public Task RunAsync(params string[] args)
            => BuildHost(args).RunAsync();

        private IHost BuildHost(string[] args)
            => Host.CreateDefaultBuilder(args)
				   .ConfigureWebHostDefaults(webBuilder
                        => webBuilder.UseStartup<ApiAppStartup>(ctx
                            => new ApiAppStartup(_configureServices, _configure)))
                    .Build();
    }
}