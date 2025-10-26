using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _storage;
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public JwtAuthStateProvider(ILocalStorageService storage) => _storage = storage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storage.GetItemAsStringAsync("authToken");
        if (string.IsNullOrWhiteSpace(token) || IsExpired(token))
            return new AuthenticationState(_anonymous);

        var user = new ClaimsPrincipal(new ClaimsIdentity(ParseClaims(token), "jwt"));
        return new AuthenticationState(user);
    }

    public async Task MarkUserAuthenticated(string token)
    {
        await _storage.SetItemAsStringAsync("authToken", token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(ParseClaims(token), "jwt"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserLoggedOut()
    {
        await _storage.RemoveItemAsync("authToken");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private static IEnumerable<Claim> ParseClaims(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }

    private static bool IsExpired(string jwt)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        var exp = token.Payload.Exp; // Unix time (s)
        if (exp is null) return false;
        var expires = DateTimeOffset.FromUnixTimeSeconds((long)exp).UtcDateTime;
        return expires <= DateTime.UtcNow;
    }
}
