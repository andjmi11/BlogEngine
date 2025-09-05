using BlogAPI.Shared.Features.BlogPosts.Commands;
using BlogAPI.Shared.Features.BlogPosts.DTOs;
using BlogApp.Components.Helpers;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Components.Services
{
    public class BlogPostService
    {
        private readonly HttpClient _httpClient;

        public BlogPostService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<MethodResult> CreateBlogPostAsync(CreateBlogPostCommand command)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/BlogPost", command);
                if (response.IsSuccessStatusCode)
                {
                    var createdPost = await response.Content.ReadFromJsonAsync<PostDTO>();
                    if (createdPost == null)
                        return MethodResult.Failure("Failed to read response.");

                    return MethodResult.Success();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return MethodResult.Failure(error);
                }
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<MethodResult> DeleteBlogPostAsync(int id)
        {
            try
            {


                var response = await _httpClient.DeleteAsync($"api/BlogPost/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return MethodResult.Success();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return MethodResult.Failure(error);
                }
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<MethodResult?> UpdateBlogPostAsync(int id, UpdateBlogPostCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/BlogPost/{id}", command);
            if (response.IsSuccessStatusCode)
            {
                return MethodResult.Success();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return MethodResult.Failure(error);
            }
        }

        public async Task<IEnumerable<PostDTO>> GetPostsAsync(
            List<string>? tags = null,
            string? language = null,
            int? authorId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null){ 
            string url = "api/BlogPost/get-all";

            var queryParams = new List<string>();

            if (tags != null && tags.Any())
            {
                var csv = string.Join(",", tags);
                queryParams.Add($"tags={Uri.EscapeDataString(csv)}");
            }

            if (!string.IsNullOrEmpty(language))
                queryParams.Add($"language={Uri.EscapeDataString(language)}");

            if (authorId.HasValue)
                queryParams.Add($"authorId={authorId.Value}");

            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");

            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);


            return await _httpClient.GetFromJsonAsync<IEnumerable<PostDTO>>(url) ?? Enumerable.Empty<PostDTO>();
        }
        public async Task<PostDTO?> GetPostByIdAsync(int id)
        {
            try
            {
                var post = await _httpClient.GetFromJsonAsync<PostDTO>($"api/BlogPost/{id}");
                return post;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching post: {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<TagDTO>> GetTagsByBlogIdAsync(int blogId)
        {
            try
            {
                var tags = await _httpClient.GetFromJsonAsync<IEnumerable<TagDTO>>($"api/BlogPost/{blogId}/tags");
                return tags ?? Enumerable.Empty<TagDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tags for blog {blogId}: {ex.Message}");
                return Enumerable.Empty<TagDTO>();
            }
        }

        public async Task<IEnumerable<string>> GetLanguagesAsync()
        {
            try
            {
                var languages = await _httpClient.GetFromJsonAsync<IEnumerable<string>>("api/BlogPost/languages");
                return languages ?? Enumerable.Empty<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching languages: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

    }
}
