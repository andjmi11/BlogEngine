using BlogAPI.Shared.Features.Authors.DTOs;
using BlogAPI.Shared.Features.BlogPosts.Commands;
using BlogAPI.Shared.Features.BlogPosts.Mapping;
using BlogApp.Components.Helpers;
using BlogApp.Components.Pages.Forms;
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
        [Parameter] public int? UrlPostId { get; set; }
        [Inject] public BlogPostService BlogPostService { get; set; }
        [Inject] public AuthorService AuthorService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public int BlogId => UrlPostId ?? 0;
        private string _tags;
        private List<AuthorDTO> Authors = new();
        Blazored.TextEditor.BlazoredTextEditor QuillHtml { get; set; }
        string QuillHTMLContent;
        private AlertModal? alertModal;

        private async Task HandleAuthorSaved(MethodResult result)
        {
            if (result.Status == true)
            {
                Authors = await AuthorService.GetAllAuthorsAsync();

                var lastAuthor = Authors.LastOrDefault();
                if (lastAuthor != null)
                {
                    _blogModel.AuthorId = lastAuthor.Id;
                }
            }

            StateHasChanged();
        }

        private async Task OpenAuthorForm()
        {
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.showModal", "authorModal");
        }

        private async Task CloseAuthorForm()
        {
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.hideModal", "authorModal");
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
                if (result?.Status == true)
                {
                    NavigationManager.NavigateTo("/manage-blogs", forceLoad: true);
                }

            }
            else
            {
                await alertModal.ShowAsync($"Error: {result?.ErrorMessage ?? "Unknown error"}");
            }
        }

        private async Task DeleteBlogPostAsync()
        {
            if (BlogId > 0)
            {
                var result = await BlogPostService.DeleteBlogPostAsync(BlogId);

                if (result.Status == true)
                {
                    if (result.Status == true)
                    {
                        NavigationManager.NavigateTo("/manage-blogs", forceLoad: true);
                    }

                }
                else
                {
                    await alertModal.ShowAsync($"Error: {result.ErrorMessage ?? "Unknown error"}");
                }
            }
        }
        private void Cancel()
        {
            _blogModel = new CreateBlogPostCommand();  
            _ = QuillHtml.LoadHTMLContent(string.Empty); 
        }
    }
}
