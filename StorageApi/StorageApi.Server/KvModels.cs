namespace StorageApi.Server;

public sealed class SetValueRequest
{
    public string Value { get; set; }
}

public sealed class GetValueResponse
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public sealed class ListKeysResponse
{
    public string[] Keys { get; set; }
}
