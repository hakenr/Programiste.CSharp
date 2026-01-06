using System.Collections.Concurrent;

namespace StorageApi.Server;

public interface IKvStore
{
    bool TryGet(string key, out string value);
    bool Upsert(string key, string value);
    bool TryRemove(string key);
    string[] ListKeys(string prefix);
}

public sealed class InMemoryKvStore : IKvStore
{
    private readonly ConcurrentDictionary<string, string> _data = new();

    public bool TryGet(string key, out string value)
        => _data.TryGetValue(key, out value);

    public bool Upsert(string key, string value)
        => _data.TryAdd(key, value) ? true : (_data[key] = value) is not null && false;

    public bool TryRemove(string key)
        => _data.TryRemove(key, out _);

    public string[] ListKeys(string prefix)
        => _data.Keys.OrderBy(k => k).ToArray();
}
