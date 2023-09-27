using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationState { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public bool NotAuthorised { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationState;
            if(authState?.User?.Identity == null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
                if(string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("login", true);
                }
                else
                {
                    navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                NotAuthorised = true;
            }
        }
    }
}
