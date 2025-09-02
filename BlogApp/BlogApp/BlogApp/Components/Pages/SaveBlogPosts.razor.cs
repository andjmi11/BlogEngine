using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.BlogPosts.Commands;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BlogApp.Components.Pages
{
    public partial class SaveBlogPosts : ComponentBase
    {

        private CreateBlogPostCommand _blogModel = new CreateBlogPostCommand
        {
            DatePublished = DateTime.Today
        };
        private UpdateBlogPostCommand _updateBlogModel = new UpdateBlogPostCommand
        {
            DatePublished = DateTime.Today
        };
        private string _tags;
        private List<AuthorDTO> Authors = new();
        [Parameter]
        public int? UrlPostId { get; set; }
        public int BlogId => UrlPostId ?? 0;
        [Inject]
        public BlogPostService BlogPostService { get; set; }
        [Inject]
        public AuthorService AuthorService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        Blazored.TextEditor.BlazoredTextEditor QuillHtml { get; set; }
        string QuillHTMLContent;

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
                _updateBlogModel.Title = _blogModel.Title;
                _updateBlogModel.ShortDescription = _blogModel.ShortDescription;
                _updateBlogModel.Content = _blogModel.Content;
                _updateBlogModel.Language = _blogModel.Language;
                _updateBlogModel.Tags = _blogModel.Tags;
                _updateBlogModel.DatePublished = _blogModel.DatePublished;
                _updateBlogModel.AuthorId = _blogModel.AuthorId;
                result = await BlogPostService.UpdateBlogPostAsync(BlogId, _updateBlogModel);
            }
            else
            {
                result = await BlogPostService.CreateBlogPostAsync(_blogModel);
            }

            if (result?.Status == true)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Blog post saved successfully!");
                NavigationManager.NavigateTo("/manage-blogs");
            }
            else
            {
                await JSRuntime.InvokeVoidAsync(
                    "alert",
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
                    await JSRuntime.InvokeVoidAsync("alert", "Blog post saved successfully!");
                    NavigationManager.NavigateTo("/manage-blogs");
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync(
                        "alert",
                        $"Error: {result.ErrorMessage ?? "Unknown error"}"
                    );
                }
            }
        }



    }
}
