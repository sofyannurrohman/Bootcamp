using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace TeamTaskTracker.UI.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    private const string TokenKey = "authToken";

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var loginModel = new { Email = email, Password = password };
        var response = await _http.PostAsJsonAsync("api/User/Login", loginModel);

        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result == null) return false;

        await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", result.Token);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        _http.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> IsLoggedInAsync()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
        if (!string.IsNullOrWhiteSpace(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
        return false;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }

    private class LoginResult
    {
        public string Token { get; set; } = null!;
    }
}
