using System.Text.RegularExpressions;

namespace StorageApi.Server;

public static class KvValidation
{
    private static readonly Regex KeyRegex =
        new Regex("^[a-zA-Z0-9:_-]{1,50}$", RegexOptions.Compiled);

    public static bool IsValidKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return false;
        }

        return KeyRegex.IsMatch(key);
    }

    public static bool IsValidValue(string value)
    {
        if (value == null)
        {
            return false;
        }

        return value.Length <= 2000;
    }
}
