using DecoratorPattern.Application.Interfaces;

namespace DecoratorPattern.API.Endpoints;

public class UserEndpoints : IEndpoint
{
	public void MapEndpoints(IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/users");
		group.MapGet("/{id:guid}", GetUserByIdAsync);
	}

	private static async Task<IResult> GetUserByIdAsync(Guid id, IUserService userService, CancellationToken cancellationToken)
	{
		var user = await userService.GetUserAsync(id, cancellationToken);
		return user is not null ? Results.Ok(user) : Results.NotFound();
	}
}
