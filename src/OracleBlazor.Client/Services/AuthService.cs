using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly JwtAuthStateProvider _authState;

    public AuthService(IHttpClientFactory factory, AuthenticationStateProvider provider)
    {
        _http = factory.CreateClient("Api");
        _authState = (JwtAuthStateProvider)provider;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var res = await _http.PostAsJsonAsync("user/login", new { username, password });
        if (!res.IsSuccessStatusCode) return false;

        // API’nizin döndürdüğü şemaya göre değiştirin
        var dto = await res.Content.ReadFromJsonAsync<LoginResponse>();
        if (string.IsNullOrWhiteSpace(dto?.Token)) return false;

        await _authState.MarkUserAuthenticated(dto!.Token);
        return true;
    }

    public async Task LogoutAsync() => await _authState.MarkUserLoggedOut();

    private record LoginResponse(string Token);
}
