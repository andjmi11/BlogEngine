using BlogAPI.Shared.Features.BlogPosts.DTOs;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;

namespace BlogApp.Components.Pages
{
    public partial class BlogPostDetails:ComponentBase
    {
        [Parameter] public int UrlPostId { get; set; }
        [Inject] private BlogPostService BlogPostService { get; set; }
        private PostDTO post = new();
        private bool notFound = false;

        protected override async Task OnInitializedAsync()
        {
            post = await BlogPostService.GetPostByIdAsync(UrlPostId);
            if (post == null)
            {
                notFound = true;
            }

        }
    }
}
