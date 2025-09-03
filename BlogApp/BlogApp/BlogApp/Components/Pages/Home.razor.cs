using BlogAPI.Features.BlogPosts.DTOs;
using Microsoft.AspNetCore.Components;
using BlogApp.Components.Services;

namespace BlogApp.Components.Pages
{
    public partial class Home:ComponentBase
    {
        private IEnumerable<PostDTO> posts = null;
        private Dictionary<int, IEnumerable<TagDTO>> postTags = new();
        [Inject]
        public BlogPostService BlogPostService { get; set; }

        private string? selectedLanguage;
        private List<string> selectedTags = new();
        private List<string> availableLanguages = new();

        protected override async Task OnInitializedAsync()
        {
            availableLanguages = (await BlogPostService.GetLanguagesAsync()).ToList();
            await LoadPostsAsync();
        }

        private async Task LoadPostsAsync()
        {
            posts = (await BlogPostService.GetPostsAsync(selectedTags, selectedLanguage))
                        .ToList();

            if (posts != null)
            {
                postTags.Clear();
                foreach (var post in posts)
                {
                    var tags = await BlogPostService.GetTagsByBlogIdAsync(post.Id);
                    postTags[post.Id] = tags;
                }
            }
        }

        private async Task OnLanguageChanged(ChangeEventArgs e)
        {
            selectedLanguage = e.Value?.ToString();
            await LoadPostsAsync();
        }


        private async Task OnTagClicked(string tag)
        {
            if (!selectedTags.Contains(tag))
                selectedTags.Add(tag);

            await LoadPostsAsync();
        }

        private async Task RemoveTag(string tag)
        {
            selectedTags.Remove(tag);
            await LoadPostsAsync();
        }

        private async void ClearAllFilters()
        {
            selectedTags.Clear();
            selectedLanguage = null;
            await LoadPostsAsync();
            StateHasChanged();
        }
    }
}
