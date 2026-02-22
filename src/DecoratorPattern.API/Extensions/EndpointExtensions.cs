using DecoratorPattern.API.Endpoints;
using System.Reflection;

namespace DecoratorPattern.API.Extensions;

public static class EndpointExtensions
{
	public static IServiceCollection AddEndpoints(this IServiceCollection services)
	{
		var endpointTypes = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => typeof(IEndpoint).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });

		foreach (var type in endpointTypes)
			services.AddScoped(typeof(IEndpoint), type);

		return services;
	}

	public static IApplicationBuilder MapEndpoints(this WebApplication app)
	{
		var endpoints = app.Services
			.CreateScope().ServiceProvider
			.GetRequiredService<IEnumerable<IEndpoint>>();

		foreach (var endpoint in endpoints)
			endpoint.MapEndpoints(app);

		return app;
	}
}