using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace RoutingRecords
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds services for objecs of type <see cref="RouteRecord"/> to the specified <see cref="IServiceCollection" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
		/// <param name="assemblies">The assemblies where it will look for object of type <see cref="RouteRecord"/>.</param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		public static IServiceCollection AddRoutes(this IServiceCollection services, params Assembly[] assemblies)
		{
			assemblies
					.SelectMany(a =>
						a.GetTypes()
						 .Where(type => !type.IsAbstract && typeof(RouteRecord).IsAssignableFrom(type))
					)
					.ToList()
					.ForEach(type =>
					{
						services.AddScoped(type);
						services.AddScoped(s => (RouteRecord)s.GetService(type));
					});

			return services;
		}

		/// <summary>
		/// Adds services for objecs of type <see cref="RouteRecord"/> to the specified <see cref="IServiceCollection" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		/// <remarks>It will look for objects of type <see cref="RouteRecord"/> in all loaded assemblies.</remarks>
		public static IServiceCollection AddRoutes(this IServiceCollection services)
		{
			var assemblies = Assembly.GetEntryAssembly()
									 .GetReferencedAssemblies()
									 .Select(Assembly.Load)
									 .ToArray();

			return services.AddRoutes(assemblies);
		}
	}
}
