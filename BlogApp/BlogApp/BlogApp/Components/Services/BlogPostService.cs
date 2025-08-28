using BlogAPI.Features.BlogPosts.Commands;
using BlogAPI.Features.BlogPosts.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlogApp.Components.Services
{
    public class BlogPostService
    {
        private readonly HttpClient _httpClient;

        public BlogPostService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<int> CreateBlogPostAsync(CreateBlogPostCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/BlogPost", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/BlogPost/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<string?> UpdateBlogPostAsync(int id, UpdateBlogPostCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/BlogPost/{id}", command);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
            }

            return await response.Content.ReadFromJsonAsync<string>();
        }

        public async Task<IEnumerable<PostDTO>> GetPostsAsync(List<string>? tags = null, string? language = null)
        {
            string url = "api/BlogPost/get-all";

            if ((tags != null && tags.Any()) || !string.IsNullOrEmpty(language))
            {
                var queryParams = new List<string>();
                if (tags != null && tags.Any())
                    queryParams.AddRange(tags.Select(tag => $"tags={Uri.EscapeDataString(tag)}"));

                if (!string.IsNullOrEmpty(language))
                    queryParams.Add($"language={Uri.EscapeDataString(language)}");

                url += "?" + string.Join("&", queryParams);
            }

            return await _httpClient.GetFromJsonAsync<IEnumerable<PostDTO>>(url) ?? Enumerable.Empty<PostDTO>();
        }
    }
}
