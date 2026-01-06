using StorageApi.Client;

var baseUrl = "https://localhost:62957";
var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

var kv = new KvClient(http);

if (args.Length == 0)
{
	Console.WriteLine("Interactive mode. Type 'exit' or 'quit' to exit.");
	while (true)
	{
		Console.Write("> ");
		var input = Console.ReadLine();

		if (string.IsNullOrWhiteSpace(input))
			continue;

		if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
			input.Equals("quit", StringComparison.OrdinalIgnoreCase))
			break;

		var inputArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

		if (!CliParser.TryParse(inputArgs, out var command, out var error))
		{
			Console.WriteLine(error);
			continue;
		}

		await ExecuteCommandAsync(kv, command);
	}

	return;
}

if (!CliParser.TryParse(args, out var cmd, out var parseError))
{
	Console.WriteLine(parseError);
	CliParser.PrintHelp();
	return;
}

await ExecuteCommandAsync(kv, cmd);

static async Task ExecuteCommandAsync(KvClient kv, CliCommand command)
{
	switch (command.Name)
	{
		case "set":
			await kv.SetAsync(command.Key, command.Value);
			Console.WriteLine("OK");
			break;
		case "get":
			var value = await kv.GetAsync(command.Key);
			Console.WriteLine(value ?? "NOT FOUND");
			break;
		case "del":
			var deleted = await kv.DeleteAsync(command.Key);
			Console.WriteLine(deleted ? "DELETED" : "NOT FOUND");
			break;
		case "list":
			var keys = await kv.ListAsync(command.Prefix);
			foreach (var k in keys) Console.WriteLine(k);
			break;
	}
}
