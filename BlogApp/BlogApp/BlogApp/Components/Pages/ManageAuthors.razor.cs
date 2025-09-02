using BlogAPI.Features.Authors.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BlogApp.Components.Pages
{
    public partial class ManageAuthors : ComponentBase
    {
        private const string _authorFormDialog = "category-form";
        private bool loading = false;
        private IEnumerable<AuthorDTO> authors = null;
        private AuthorDTO editAuthor = new();
        [Inject]
        public AuthorService AuthorService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
  
        protected override async Task OnInitializedAsync()
        {
            await LoadAuthorsAsync();
        }
        private async Task LoadAuthorsAsync()
        {
            loading = true;
            try
            {
                authors = await AuthorService.GetAllAuthorsAsync();
            }
            finally
            {
                loading = false;
            }
        }
        private async Task OnSaveAuthor(MethodResult saveAuthorResult)
        {
            if (saveAuthorResult.Status)
            {
                await AlertAsync("Author saved successfully");
                await LoadAuthorsAsync();
            }
            else
            {
                await AlertAsync(saveAuthorResult.ErrorMessage);
            }
        }

        private async Task OnDeleteAuthor(MethodResult deleteAuthorResult)
        {
            if (deleteAuthorResult.Status)
            {
                await AlertAsync("Author deleted successfully");
                await LoadAuthorsAsync();
            }
            else
            {
                await AlertAsync(deleteAuthorResult.ErrorMessage);
            }
        }

        private async Task AlertAsync(string message)
        {
            await JSRuntime.InvokeVoidAsync("window.alert", message);
        }
        private async Task OpenAuthorForm()
        { 
            await JSRuntime.InvokeVoidAsync("window.openModal", _authorFormDialog);
        }

        private async Task CloseAuthorForm()
        {
            editAuthor = new();
            await JSRuntime.InvokeVoidAsync("window.closeModal", _authorFormDialog);
        }

        private async Task EditAuthor(AuthorDTO authorUpdate)
        {
            editAuthor = authorUpdate.Clone();
            await OpenAuthorForm();
        }
    }
}
