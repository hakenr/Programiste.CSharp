using Microsoft.Extensions.Logging;
using Refit;
using ReqResRefitDemo.ApiClient;
using ReqResRefitDemo.HttpAuth;
using ReqResRefitDemo.Models;

namespace ReqResRefitDemo;

public sealed class App
{
	private readonly ApiKeyProvider _apiKeyProvider;
	private readonly TokenProvider _tokenProvider;
	private readonly IAuthApi _authApi;
	private readonly IUsersApi _usersApi;
	private readonly ILogger<App> _logger;

	public App(
		ApiKeyProvider apiKeyProvider,
		TokenProvider tokenProvider,
		IAuthApi authApi,
		IUsersApi usersApi,
		ILogger<App> logger)
	{
		_apiKeyProvider = apiKeyProvider;
		_tokenProvider = tokenProvider;
		_authApi = authApi;
		_usersApi = usersApi;
		_logger = logger;
	}

	public async Task RunAsync()
	{
		Console.Write("Enter x-api-key: ");
		_apiKeyProvider.ApiKey = Console.ReadLine();

		Console.WriteLine();

		Console.WriteLine("== LOGIN ==");
		try
		{
			var login = await _authApi.Login(new LoginRequest
			{
				Email = "eve.holt@reqres.in",
				Password = "cityslicka"
			});

			_tokenProvider.BearerToken = login.Token;
			Console.WriteLine($"Token: {login.Token}");
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
			return;
		}

		Console.WriteLine();
		Console.WriteLine("== GET USERS ==");
		try
		{
			var users = await _usersApi.GetUsers(page: 1);
			foreach (var u in users.Data)
			{
				Console.WriteLine($"{u.Id}: {u.FirstName} {u.LastName} ({u.Email})");
			}
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
		}

		Console.WriteLine();
		Console.WriteLine("== GET USER (id=2) ==");
		try
		{
			var user = await _usersApi.GetUser(2);
			Console.WriteLine($"{user.Data.Id}: {user.Data.FirstName} {user.Data.LastName} ({user.Data.Email})");
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
		}

		Console.WriteLine();
		Console.WriteLine("== CREATE USER ==");
		try
		{
			var created = await _usersApi.CreateUser(new UserMutationRequest
			{
				Name = "Petr",
				Job = "Developer"
			});

			Console.WriteLine($"Created: id={created.Id}, createdAt={created.CreatedAt}");
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
		}

		Console.WriteLine();
		Console.WriteLine("== UPDATE USER (id=2) ==");
		try
		{
			var updated = await _usersApi.UpdateUser(2, new UserMutationRequest
			{
				Name = "Petr",
				Job = "Senior Developer"
			});

			Console.WriteLine($"Updated: updatedAt={updated.UpdatedAt}");
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
		}

		Console.WriteLine();
		Console.WriteLine("== DELETE USER (id=2) ==");
		try
		{
			await _usersApi.DeleteUser(2);
			Console.WriteLine("Deleted (204 No Content)");
		}
		catch (ApiException ex)
		{
			await PrintApiErrorAsync(ex);
		}
	}

	private async Task PrintApiErrorAsync(ApiException ex)
	{
		var content = ex.HasContent ? ex.Content : null;
		_logger.LogError("HTTP {StatusCode}. {Message}. Body: {Body}", (int)ex.StatusCode, ex.Message, content);
		Console.WriteLine($"ERROR {(int)ex.StatusCode} {ex.StatusCode}");
		if (!string.IsNullOrWhiteSpace(content))
		{
			Console.WriteLine(content);
		}
	}
}
