using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBlog.WebAPIAccess
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;

        public BlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BlogPostEntityDTO>> GetAllPosts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Posts/");
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            List<BlogPostEntityDTO> posts = await response.Content.ReadFromJsonAsync<List<BlogPostEntityDTO>>();
            return posts;
        }

        public async Task<List<TagEntityDTO>> GetAllTags()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Tags");
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            List<TagEntityDTO> tags = await response.Content.ReadFromJsonAsync<List<TagEntityDTO>>();
            return tags;
        }

        public async Task<BlogPostEntityDTO> GetPostWithId(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Posts/{id}");
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            BlogPostEntityDTO post = await response.Content.ReadFromJsonAsync<BlogPostEntityDTO>();
            return post;
        }

        public async Task InsertNewPost(BlogPostEntityDTO post)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Posts", post);
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
                string error = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(error);
                return await responseMessage.Content.ReadFromJsonAsync<ErrorMessage>();
            }
            catch
            {
                return new ErrorMessage { Message = "Unknown Error" };
            }
        }
    }
}
