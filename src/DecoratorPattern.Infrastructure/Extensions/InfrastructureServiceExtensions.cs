using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorPattern.Application.Extensions;

public static class InfrastructureServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}
}
