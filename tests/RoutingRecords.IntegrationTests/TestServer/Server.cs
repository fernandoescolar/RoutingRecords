using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace RoutingRecords.IntegrationTests.TestServer
{
    public class Server : WebApplicationFactory<Startup>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            return base.CreateHost(builder);
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host
                            .CreateDefaultBuilder()
                            .ConfigureWebHostDefaults(x =>
                            {
                                x.UseStartup<Startup>()
                                 .UseTestServer();
                            });
            return builder;
        }
    }
}
