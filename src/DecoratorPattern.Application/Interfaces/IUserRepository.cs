using DecoratorPattern.Domain.Entities;

namespace DecoratorPattern.Application.Interfaces;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
