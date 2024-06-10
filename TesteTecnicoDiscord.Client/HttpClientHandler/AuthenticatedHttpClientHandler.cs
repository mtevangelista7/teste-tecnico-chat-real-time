using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace TesteTecnicoDiscord.Client.HttpClientHandler;

public class AuthenticatedHttpClientHandler(ILocalStorageService localStorageService) : DelegatingHandler
{
    private const string LocalStorageKey = "AuthToken";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Get the token from local storage
        var token = await localStorageService.GetItemAsync<string>(LocalStorageKey, cancellationToken);

        // If the token is not null or empty, add it to the request's Authorization header
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Continue sending the request, without the tokens
        return await base.SendAsync(request, cancellationToken);
    }
}