using MyCrm.Shared.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MyCrm.Shared.Models;

namespace MyCrm.Shared.Blazor.Authentication;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    public string? Token { get; private set; }

    private static readonly ClaimsPrincipal anonymousUser = new(new ClaimsIdentity());
    private ClaimsPrincipal currentUser = anonymousUser;

    private readonly HttpClient httpClient;

    public JwtAuthenticationStateProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(currentUser));

    public async Task LoginAsync(LoginModel loginModel)
    {
        Token = null;
        currentUser = anonymousUser;

        var RequestBody = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("client_id", loginModel.ClientId),
            new KeyValuePair<string, string>("grant_type", loginModel.GrantType),
            new KeyValuePair<string, string>("username", loginModel.UserName),
            new KeyValuePair<string, string>("password", loginModel.Password),
            new KeyValuePair<string, string>("scope", loginModel.Scope)
        };

        var requestContent = new FormUrlEncodedContent(RequestBody);

        HttpResponseMessage response = await httpClient.PostAsync("/connect/token", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("The login attempt failed.");
            }
            else if (response.StatusCode != HttpStatusCode.NotFound)
            {
                string? message = await response.Content.ReadFromJsonAsync<string>();
                throw new Exception(message);
            }

            response.EnsureSuccessStatusCode();
        }

        var content = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

        string? token;
        if (content == null)
            token = "";
        else
            token = content.Access_Token;

        if (token == null)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));

            throw new Exception("The login attempt failed.");
        }

        JwtSecurityToken jwt = new(token);

        Claim? expiryClaim = jwt.Claims
            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);

        if (!long.TryParse(expiryClaim?.Value, out long expiryUnixTimeSeconds))
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));

            throw new Exception("The login attempt failed.");
        }

        DateTimeOffset expiry = DateTimeOffset.FromUnixTimeSeconds(expiryUnixTimeSeconds);

        if (expiry <= DateTimeOffset.UtcNow)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));

            throw new Exception("The login attempt failed.");
        }

        ClaimsIdentity loggedInUserIdentity = new(jwt.Claims, "jwt");
        Claim? nameClaim = loggedInUserIdentity.FindFirst(JwtRegisteredClaimNames.UniqueName);
        Claim? nameIdClaim = loggedInUserIdentity.FindFirst(JwtRegisteredClaimNames.Sub);

        if (nameClaim != null && nameClaim.Value != null)
        {
            loggedInUserIdentity.AddClaim(new(ClaimTypes.Name, nameClaim.Value));

            if (nameIdClaim != null && nameIdClaim.Value != null)
            {
                HttpRequestMessage requestRoles = new(HttpMethod.Get, $"api/identity/users/{nameIdClaim.Value}/roles");
                requestRoles.Headers.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage responseRoles = await httpClient.SendAsync(requestRoles);

                if (responseRoles != null && responseRoles.StatusCode == HttpStatusCode.OK)
                {
                    RoleItemsDto<RoleItems>? roleItems = await responseRoles.Content.ReadFromJsonAsync<RoleItemsDto<RoleItems>>();
                    var roleNames = roleItems?.Items?.Select(item => item.Name).ToList();
                    if (roleNames != null)
                    {
                        foreach (string roleName in roleNames)
                        {
                            loggedInUserIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                        }
                    }
                }
            }
        }

        Token = token;
        currentUser = new ClaimsPrincipal(loggedInUserIdentity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentUser)));
    }

    public void Logout()
    {
        Token = null;
        currentUser = anonymousUser;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentUser)));
    }
}