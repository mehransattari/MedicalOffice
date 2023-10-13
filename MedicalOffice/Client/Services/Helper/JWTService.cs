using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace MedicalOffice.Client.Services.Helper;

public class JWTService : AuthenticationStateProvider, IUserAuthService
{
    #region Constructor
    private readonly IJSRuntime _jsRuntime;
    private readonly string _tokenKey = "token";
    private readonly string _expirationKey = "expirationKey";
    private readonly HttpClient _http;
    public JWTService(IJSRuntime jsRuntime, HttpClient http)
    {
        _jsRuntime = jsRuntime;
        _http = http;
    }
    #endregion

    #region Methods
    private AuthenticationState EmptyUserData()
    {
        // Empty User Data
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _jsRuntime.GetItem(_tokenKey);
        if (string.IsNullOrEmpty(token))
        {
            return EmptyUserData();
        }

        if (!await CheckToken())
        {
            await CleanUp();
            return EmptyUserData();
        }

        return BuildAuth(token);
    }
    public async Task<bool> CheckToken()
    {
        string expiration = await _jsRuntime.GetItem(_expirationKey);
        DateTime nowDate = DateTime.Now;
        if (!string.IsNullOrEmpty(expiration))
        {
            DateTime targetDate = Convert.ToDateTime(expiration);
            var diff = targetDate.Subtract(nowDate).TotalSeconds;
            if (diff > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public async Task CleanUp()
    {
        await _jsRuntime.RemoveItem(_tokenKey);
        await _jsRuntime.RemoveItem(_expirationKey);
        _http.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(EmptyUserData()));
    }
    public AuthenticationState BuildAuth(string jwtToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwtToken);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(jwtToken), "jwt")));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var bytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        return claims;
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    // For IUserAuthService
    public async Task Login(TokenData tokenData)
    {
        await _jsRuntime.SetItem(_tokenKey, tokenData.Token);
        await _jsRuntime.SetItem(_expirationKey, tokenData.Expiration.ToString());
        var authState = BuildAuth(tokenData.Token);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        await CleanUp();
        NotifyAuthenticationStateChanged(Task.FromResult(EmptyUserData()));

    }
    #endregion
}

