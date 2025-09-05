using BlogAPI.Shared.Features.BlogPosts.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Pages.Forms;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace BlogApp.Components.Pages
{
    public partial class ManageBlogPosts : ComponentBase
    {
        private bool loading = false;
        private IEnumerable<PostDTO> posts = null;
        [Inject] public BlogPostService BlogPostService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPostsAsync();
        }


        private async Task LoadPostsAsync()
        {
            loading = true;
            try
            {
                posts = await BlogPostService.GetPostsAsync();
            }
            finally
            {
                loading = false;
            }
        }

    }
}
