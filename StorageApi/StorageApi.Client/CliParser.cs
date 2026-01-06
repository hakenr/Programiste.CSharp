namespace StorageApi.Client;

public static class CliParser
{
    public static bool TryParse(string[] args, out CliCommand command, out string error)
    {
        command = null;
        error = null;

        if (args.Length == 0)
        {
            error = "Missing command.";
            return false;
        }

        var name = args[0].ToLowerInvariant();

        if (name == "set" && args.Length >= 3)
        {
            command = CliCommand.Set(args[1], string.Join(" ", args.Skip(2)));
            return true;
        }

        if (name == "get" && args.Length == 2)
        {
            command = CliCommand.Get(args[1]);
            return true;
        }

        if (name == "del" && args.Length == 2)
        {
            command = CliCommand.Del(args[1]);
            return true;
        }

        if (name == "list")
        {
            command = CliCommand.List(args.Length >= 2 ? args[1] : null);
            return true;
        }

        error = "Invalid command.";
        return false;
    }

    public static void PrintHelp()
    {
        Console.WriteLine("Commands: set <key> <value>, get <key>, del <key>, list [prefix]");
    }
}

public sealed class CliCommand
{
    private CliCommand(string name) => Name = name;

    public string Name { get; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string Prefix { get; private set; }

    public static CliCommand Set(string key, string value) => new("set") { Key = key, Value = value };
    public static CliCommand Get(string key) => new("get") { Key = key };
    public static CliCommand Del(string key) => new("del") { Key = key };
    public static CliCommand List(string prefix) => new("list") { Prefix = prefix };
}
