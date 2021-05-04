using AuthorizedApp.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RoutingRecords;
using System.Linq;

namespace AuthorizedApp
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRouteRecords();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.Audience = "https://localhost:5001/";
						options.Authority = "https://demo.identityserver.io/";
					});
			services.AddAuthorization();

			// all endpoints authorization required policy
			// services.AddAuthorization(options =>
			// {
			//     options.FallbackPolicy = new AuthorizationPolicyBuilder()
			//         .RequireAuthenticatedUser()
			//         .Build();
			// });
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapRouteRecords()
												   .Where(x => x.RouteRecordType.IsNot<Hello>())
												   .ToList()
												   .ForEach(y => y.RequireAuthorization()));

			// if you have defined a global policy you don't need to configure any convention
			//app.UseEndpoints(endpoints => endpoints.MapRouteRecords());
		}
	}
}
