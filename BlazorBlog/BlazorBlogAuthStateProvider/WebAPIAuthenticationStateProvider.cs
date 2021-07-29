using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorBlog.BlazorBlogAuthStateProvider
{
    public class WebAPIAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly IAdminService _adminService;
        private readonly ICurrentUserService _currentUserService;

        public WebAPIAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient, IAdminService adminService, ICurrentUserService currentUserService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _adminService = adminService;
            _currentUserService = currentUserService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string savedToken = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("bearer", savedToken);
                UserDTO user = await _adminService.GetUserByTokenAsync();
                user.Token = savedToken;
                return await CreateAuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        public void SetAuthenticatedState(UserDTO user)
        {
            Task<AuthenticationState> authStateTask = CreateAuthenticationState(user);
            NotifyAuthenticationStateChanged(authStateTask);
        }
        private async Task<AuthenticationState> CreateAuthenticationState(UserDTO user)
        {
            await _localStorageService.SetItemAsync("authToken", user.Token);
            _currentUserService.CurrentUser = user;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.Token);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(user.Token);

            System.Collections.Generic.List<Claim> claims = token.Claims.ToList();
            Claim roleClaim = claims.FirstOrDefault(c => c.Type == "role");
            if (roleClaim != null)
            {
                Claim newRoleClaim = new Claim(ClaimTypes.Role, roleClaim.Value);
                claims.Add(newRoleClaim);
            }
            Console.WriteLine(JsonSerializer.Serialize(claims));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(claimsPrincipal);
        }
        public void UnsetUser()
        {
            NotifyAuthenticationStateChanged(CreateUnsetUserAuthenticationStateAsync());
        }
        private async Task<AuthenticationState> CreateUnsetUserAuthenticationStateAsync()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ClaimsPrincipal unsetUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(unsetUser);
        }

    }
}
