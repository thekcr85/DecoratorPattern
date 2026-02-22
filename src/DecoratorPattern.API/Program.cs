using DecoratorPattern.Application.Extensions;
using DecoratorPattern.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

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
