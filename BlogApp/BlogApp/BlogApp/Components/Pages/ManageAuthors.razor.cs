using BlogAPI.Shared.Features.Authors.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Pages.Forms;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlogApp.Components.Pages
{
    public partial class ManageAuthors : ComponentBase
    {
        private bool loading = false;
        private IEnumerable<AuthorDTO> authors = null;
        private AuthorDTO editAuthor = new();

        [Inject] public AuthorService AuthorService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        private AlertModal? alertModal;


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
            if (alertModal != null)
            {
                await alertModal.ShowAsync(message);
            }
        }

        private async Task OpenAuthorForm()
        {
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.showModal", "authorModal");
        }

        private async Task CloseAuthorForm()
        {
            editAuthor = new();
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.hideModal", "authorModal");
        }

        private async Task EditAuthor(AuthorDTO authorUpdate)
        {
            editAuthor = authorUpdate.Clone();
            await OpenAuthorForm();
        }
    }
}
