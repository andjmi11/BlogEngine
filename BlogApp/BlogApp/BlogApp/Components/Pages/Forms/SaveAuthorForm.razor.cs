using BlogAPI.Shared.Features.Authors.Commands;
using BlogAPI.Shared.Features.Authors.DTOs;
using BlogApp.Components.Helpers;
using BlogApp.Components.Services;
using Microsoft.AspNetCore.Components;
using BlogAPI.Shared.Features.Authors.Mapping;

namespace BlogApp.Components.Pages.Forms
{
    public partial class SaveAuthorForm : ComponentBase
    {
        [Parameter] public AuthorDTO? AuthorDTO { get; set; }
        [Parameter] public EventCallback<MethodResult> OnSaveAuthor { get; set; }
        [Parameter] public EventCallback OnCloseForm { get; set; }
        [Parameter] public EventCallback<MethodResult> OnDeleteAuthor { get; set; }

        [Inject] public AuthorService AuthorService { get; set; }

        private UpdateAuthorCommand _updatedAuthorModel = new();
        private CreateAuthorCommand _authorModel = new();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (AuthorDTO != null && AuthorDTO.Id > 0)
            {
                _authorModel = AuthorDTO.ToCreateCommand();
            }
        }
        private async Task SaveAuthorAsync()
        {
            if (AuthorDTO != null && AuthorDTO.Id > 0)
            {   
                _updatedAuthorModel.Id = AuthorDTO.Id;
                _updatedAuthorModel.FirstName = _authorModel.FirstName;
                _updatedAuthorModel.LastName = _authorModel.LastName;

                var result = await AuthorService.UpdateAuthorAsync(_updatedAuthorModel);
                await OnSaveAuthor.InvokeAsync(result);
            }
            else
            {
                var result = await AuthorService.CreateAuthorAsync(_authorModel);
                await OnSaveAuthor.InvokeAsync(result);
            }
            await CloseFormAsync();
        }
        private async Task CloseFormAsync()
        {
            _authorModel = new();
            _updatedAuthorModel = new();

            await OnCloseForm.InvokeAsync();
        }

        private async Task DeleteAuthorAsync()
        {
            if (AuthorDTO != null && AuthorDTO.Id > 0)
            {
                var result = await AuthorService.DeleteAuthorAsync(AuthorDTO.Id);
                await OnDeleteAuthor.InvokeAsync(result);
            }
            await CloseFormAsync();
        }
    }

}
