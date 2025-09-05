using BlogAPI.Shared.Features.Authors.DTOs;
using BlogAPI.Shared.Features.BlogPosts.DTOs;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlogApp.Components.Pages
{
    public partial class Home:ComponentBase
    {
        private IEnumerable<PostDTO> posts = null;
        private Dictionary<int, IEnumerable<TagDTO>> postTags = new();
        [Inject] public BlogPostService BlogPostService { get; set; }
        [Inject] public AuthorService AuthorService { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        private string? selectedLanguage;
        private List<string> selectedTags = new();
        private List<string> availableLanguages = new();
        private List<AuthorDTO> availableAuthors = new();
        private int? selectedAuthorId;
        private DateTime? dateFrom;
        private DateTime? dateTo;

        private string dateFromString;
        private string dateToString;
        private int filterSidebar = 0;
        private int filterSidebarMobile = 1;
        private async Task ApplyFiltersAndClose()
        {
            await JS.InvokeVoidAsync("closeOffcanvas", "mobileFilters"); 
        }
        protected override async Task OnInitializedAsync()
        {
            availableLanguages = (await BlogPostService.GetLanguagesAsync()).ToList();
            availableAuthors = (await AuthorService.GetAllAuthorsAsync()).ToList();
            await LoadPostsAsync();
        }

        private async Task LoadPostsAsync()
        {
            posts = (await BlogPostService.GetPostsAsync(
                selectedTags,
                selectedLanguage,
                selectedAuthorId,
                dateFrom,
                dateTo
            )).ToList();

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

        private async Task OnAuthorChanged(ChangeEventArgs e)
        {
            var value = e.Value?.ToString();
            selectedAuthorId = string.IsNullOrEmpty(value) ? null : int.Parse(value);
            await LoadPostsAsync();
        }
        private async Task OnTagClicked(string tag)
        {
            if (!selectedTags.Contains(tag))
                selectedTags.Add(tag);

            await LoadPostsAsync();
        }
        private async Task OnDateFromChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value?.ToString(), out var result))
                dateFrom = result;

            else
                dateFrom = null;

            await LoadPostsAsync();
        }

        private async Task OnDateToChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value?.ToString(), out var result))
                dateTo = result;

            else
                dateTo = null;

            await LoadPostsAsync();
        }
        private async Task RemoveTag(string tag)
        {
            selectedTags.Remove(tag);
            await LoadPostsAsync();
        }

        private async Task ClearAllFilters()
        {
            selectedTags.Clear();
            selectedLanguage = null;
            selectedAuthorId = null;
            dateFrom = null;
            dateTo = null;

            await LoadPostsAsync();

            filterSidebar++;
            filterSidebarMobile++;
        }
    }
}
