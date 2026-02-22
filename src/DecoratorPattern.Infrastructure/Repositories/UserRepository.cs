using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Domain.Entities;

namespace DecoratorPattern.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private static readonly List<User> _users =
	[
		new(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Alice Smith", "alice@example.com"),
		new(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Bob Jones",  "bob@example.com"),
	];

	public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
		=> Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
}