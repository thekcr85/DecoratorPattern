using DecoratorPattern.Domain.Entities;

namespace DecoratorPattern.Application.Interfaces;

public interface IUserService
{
	Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
}
