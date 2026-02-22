using DecoratorPattern.Application.Decorators;
using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorPattern.Application.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
		services.Decorate<IUserService, LoggingUserServiceDecorator>();

		return services;
	}
}