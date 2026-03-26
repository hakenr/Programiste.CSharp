using System.Net.Http.Headers;

namespace ReqResRefitDemo.HttpAuth;

public sealed class ReqResHeadersHandler : DelegatingHandler
{
    private readonly ApiKeyProvider _apiKeyProvider;
    private readonly TokenProvider _tokenProvider;

    public ReqResHeadersHandler(ApiKeyProvider apiKeyProvider, TokenProvider tokenProvider)
    {
        _apiKeyProvider = apiKeyProvider;
        _tokenProvider = tokenProvider;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.UserAgent.ParseAdd("SemPrgX-ReqRes-Refit/1.0");
        request.Headers.Accept.ParseAdd("application/json");

        if (!string.IsNullOrWhiteSpace(_apiKeyProvider.ApiKey))
        {
            request.Headers.Remove("x-api-key");
            request.Headers.Add("x-api-key", _apiKeyProvider.ApiKey);
        }

        if (!string.IsNullOrWhiteSpace(_tokenProvider.BearerToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.BearerToken);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
