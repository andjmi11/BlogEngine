using BlogAPI.Shared.Features.Authors.Commands;
using BlogAPI.Shared.Features.Authors.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Pages.Forms;

namespace BlogApp.Components.Services
{
    public class AuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _httpClient.GetFromJsonAsync<List<AuthorDTO>>("api/Author");
            return authors ?? new List<AuthorDTO>();
        }

        public async Task<MethodResult> CreateAuthorAsync(CreateAuthorCommand command)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Author", command);
                if (response.IsSuccessStatusCode)
                {
                    var createdAuthor = await response.Content.ReadFromJsonAsync<AuthorDTO>();
                    if (createdAuthor == null)
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


        public async Task<MethodResult> UpdateAuthorAsync(UpdateAuthorCommand command)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Author/{command.Id}", command);
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

        public async Task<MethodResult> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Author/{id}");
            try
            {
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

        public async Task<int?> GetAuthorIdByNameAsync(string firstName, string lastName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Author/by-name?firstName={Uri.EscapeDataString(firstName)}&lastName={Uri.EscapeDataString(lastName)}");

                if (response.IsSuccessStatusCode)
                {
                    var authorId = await response.Content.ReadFromJsonAsync<int?>();
                    return authorId;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

