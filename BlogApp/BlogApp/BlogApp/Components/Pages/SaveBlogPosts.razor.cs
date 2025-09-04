using BlogAPI.Shared.Features.Authors.DTOs;
using BlogAPI.Shared.Features.BlogPosts.Commands;
using Blazored.TextEditor;
using BlogAPI.Features.BlogPosts.Mapping;
using BlogApp.Components.Helpers;
using BlogApp.Components.Pages.Forms;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BlogApp.Components.Pages
{
    public partial class SaveBlogPosts : ComponentBase
    {

        private CreateBlogPostCommand _blogModel = new() { DatePublished = DateTime.Today };
        private UpdateBlogPostCommand _updateBlogModel = new() { DatePublished = DateTime.Today };
        private List<AuthorDTO> Authors { get; set; }
        private AlertModal alertModal;
        public int BlogId => UrlPostId ?? 0;
        BlazoredTextEditor QuillHtml { get; set; }
        string QuillHTMLContent;
        [Parameter] public int? UrlPostId { get; set; }
        [Inject] public BlogPostService BlogPostService { get; set; }
        [Inject] public AuthorService AuthorService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public async Task GetHTML()
        {
            QuillHTMLContent = await this.QuillHtml.GetHTML();
        }

        public async Task SetHTML()
        {
            string QuillContent =
                @"<a href='http://BlazorHelpWebsite.com/'>" +
                "<img src='images/BlazorHelpWebsite.gif' /></a>";

            await this.QuillHtml.LoadHTMLContent(QuillContent);
        }
        protected override async Task OnInitializedAsync()
        {
            Authors = await AuthorService.GetAllAuthorsAsync();

            if (BlogId > 0)
            {
                var post = await BlogPostService.GetPostByIdAsync(BlogId);
                if (post != null)
                {
                    _blogModel = new CreateBlogPostCommand
                    {
                        Title = post.Title,
                        ShortDescription = post.ShortDescription,
                        Content = post.Content,
                        Language = post.Language,
                        Tags = post.Tags,
                        DatePublished = post.DatePublished,
                        AuthorId = await AuthorService.GetAuthorIdByNameAsync(post.AuthorFirstName, post.AuthorLastName) ?? 0
                    };

                    await QuillHtml.LoadHTMLContent(post.Content);
                }
            }
        }
        private async Task SaveBlogAsync()
        {
            _blogModel.Content = await QuillHtml.GetHTML();

            MethodResult? result;

            if (BlogId > 0)
            {
                _updateBlogModel = _blogModel.ToUpdateCommand();
                result = await BlogPostService.UpdateBlogPostAsync(BlogId, _updateBlogModel);
            }
            else
            {
                result = await BlogPostService.CreateBlogPostAsync(_blogModel);
            }

            if (result?.Status == true)
            {
                await AlertAsync("Blog post saved successfully!");
            }
            else
            {
                await AlertAsync(
                    $"Error: {result?.ErrorMessage ?? "Unknown error"}"
                );
            }
        }

        private void Cancel()
        {
            _blogModel = new CreateBlogPostCommand();
            _ = QuillHtml.LoadHTMLContent(string.Empty);
        }

        private async Task DeleteBlogPostAsync()
        {
            if (BlogId > 0)
            {
                var result = await BlogPostService.DeleteBlogPostAsync(BlogId);

                if (result.Status == true)
                {
                    await AlertAsync("Blog post saved successfully!");
                }
                else
                {
                    await AlertAsync(
                        $"Error: {result.ErrorMessage ?? "Unknown error"}"
                    );
                }
            }
        }

        private async Task AlertAsync(string message)
        {
            if (alertModal != null)
            {
                alertModal.OnClose = EventCallback.Factory.Create(this, () =>
                {
                    NavigationManager.NavigateTo("/manage-blogs", forceLoad: true);
                });
                await alertModal.ShowAsync(message);
            }
        }
    }
}