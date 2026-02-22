using DecoratorPattern.Application.Decorators;
using DecoratorPattern.Application.Interfaces;
using DecoratorPattern.Application.Services;
using DecoratorPattern.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Decorate<IUserService, LoggingUserServiceDecorator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/users/{id:guid}", async (Guid id, IUserService userService, CancellationToken cancellationToken) =>
{
	var user = await userService.GetUserAsync(id);
	return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.Run();
