using Microsoft.AspNetCore.Mvc;
using StorageApi.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IKvStore, InMemoryKvStore>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/kv/{key}", ([FromRoute] string key, IKvStore store) =>
{
	return Results.StatusCode(StatusCodes.Status501NotImplemented);
});

app.MapPut("/kv/{key}", async ([FromRoute] string key, HttpRequest request, IKvStore store) =>
{
	await Task.CompletedTask;
	return Results.StatusCode(StatusCodes.Status501NotImplemented);
});

app.MapDelete("/kv/{key}", ([FromRoute] string key, IKvStore store) =>
{
	return Results.StatusCode(StatusCodes.Status501NotImplemented);
});

app.MapGet("/kv", ([FromQuery] string prefix, IKvStore store) =>
{
	return Results.StatusCode(StatusCodes.Status501NotImplemented);
});

app.Run();
