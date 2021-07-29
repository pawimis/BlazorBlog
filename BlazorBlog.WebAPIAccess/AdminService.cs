using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBlog.WebAPIAccess
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetUserByTokenAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/users/");
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            UserDTO foundUser = await response.Content.ReadFromJsonAsync<UserDTO>();
            return foundUser;
        }

        public async Task<UserDTO> Login(UserDTO user)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/users/login", user);
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            UserDTO userReturned = await response.Content.ReadFromJsonAsync<UserDTO>();
            if (userReturned != null && userReturned.Token != null)
            {
                return userReturned;
            }
            else
            {
                return null;
            }
        }
        public async Task Logout()
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/users/logout", new { });
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            return;
        }
        private async Task ThrowExceptionIfResponseIsNotSuccessfulAsync(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                ErrorMessage errorMessage = await TryToGetMessageAsync(responseMessage);
                throw new Exception(errorMessage.Message);
            }
        }
        private async Task<ErrorMessage> TryToGetMessageAsync(HttpResponseMessage responseMessage)
        {
            try
            {
                return await responseMessage.Content.ReadFromJsonAsync<ErrorMessage>();
            }
            catch
            {
                return new ErrorMessage { Message = "Unknown Error" };
            }
        }
    }
}
