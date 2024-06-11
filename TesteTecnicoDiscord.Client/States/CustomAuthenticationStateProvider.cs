using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace TesteTecnicoDiscord.Client.States;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
{
    private const string LocalStorageKey = "AuthToken";
    private readonly ClaimsPrincipal _anonymousUser = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // get token local storage
        var localToken = await localStorage.GetItemAsync<string>(LocalStorageKey);

        if (string.IsNullOrWhiteSpace(localToken))
            return new AuthenticationState(_anonymousUser);

        var jwtSecurityToken = new JwtSecurityToken(localToken);

        // get all claims
        var allClaims = GetClaims(jwtSecurityToken);

        if (string.IsNullOrWhiteSpace(allClaims.email)
            || string.IsNullOrWhiteSpace(allClaims.userId)
            || string.IsNullOrWhiteSpace(allClaims.username))
            return new AuthenticationState(_anonymousUser);

        // set the principal key and return the state
        var principalClaim = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, "jwtToken"));
        return new AuthenticationState(principalClaim);
    }

    private static (string userId, string username, string email) GetClaims(JwtSecurityToken token)
    {
        var userId = token.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
        var username = token.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
        var email = token.Claims.FirstOrDefault(claim => claim.Type == "email").Value;

        return (userId, username, email);
    }

    public async Task UpdateAuthenticationStateAsync(string tokenString)
    {
        var claims = new ClaimsPrincipal();
        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(tokenString))
        {
            await localStorage.RemoveItemAsync(LocalStorageKey);
            return;
        }

        var jwtSecurityToken = handler.ReadJwtToken(tokenString);

        var allClaims = GetClaims(jwtSecurityToken);

        if (string.IsNullOrWhiteSpace(allClaims.email)
            || string.IsNullOrWhiteSpace(allClaims.userId)
            || string.IsNullOrWhiteSpace(allClaims.username))
            return;

        await localStorage.SetItemAsync(LocalStorageKey, tokenString);

        // Notify authentication state changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
    }

    public async Task LogOut()
    {
        await localStorage.RemoveItemAsync(LocalStorageKey);
    }
}