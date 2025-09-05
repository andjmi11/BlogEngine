using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlogApp.Components.Pages.Forms
{
    public partial class AlertModal: ComponentBase
    {
        [Parameter] public string Message { get; set; } = string.Empty;
        [Parameter] public EventCallback OnClose { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }

        public async Task ShowAsync(string message)
        {
            Message = message;
            StateHasChanged(); 
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.showModal", "alertModal");
        }

        private async Task CloseAlert()
        {
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.hideModal", "alertModal");

            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync(null);
            }

        }
    }
}
