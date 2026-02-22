using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Domain.Entities;

namespace DecoratorPattern.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
	public Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
		=> userRepository.GetByIdAsync(id, cancellationToken);
}