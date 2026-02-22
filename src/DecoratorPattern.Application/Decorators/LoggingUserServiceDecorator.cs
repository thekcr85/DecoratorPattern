using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DecoratorPattern.Application.Decorators;

public class LoggingUserServiceDecorator(
	IUserService userService,
	ILogger<LoggingUserServiceDecorator> logger) : IUserService
{
	public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
	{
		logger.LogInformation("Fetching user with ID: {UserId}", id);

		var user = await userService.GetUserAsync(id, cancellationToken);

		if (user is not null)
			logger.LogInformation("Successfully fetched user: {UserName} ({UserEmail})", user.Name, user.Email);
		else
			logger.LogWarning("User with ID: {UserId} was not found", id);

		return user;
	}
}