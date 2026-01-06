using StorageApi.Client;

var baseUrl = "https://localhost:62957";
var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

var kv = new KvClient(http);

if (!CliParser.TryParse(args, out var command, out var error))
{
	Console.WriteLine(error);
	CliParser.PrintHelp();
	return;
}

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
