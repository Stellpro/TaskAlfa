using Microsoft.AspNetCore.Components;

namespace TaskAlfa.Shared
{
    public class RedirectToLogin : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            NavigationManager.NavigateTo(NavigationManager.BaseUri + "login");
        }
    }
}
