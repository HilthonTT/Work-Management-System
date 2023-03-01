namespace WSMPortal.Pages.Authentication
{
    public partial class Logout
    {
        protected override async Task OnInitializedAsync()
        {
            await authService.Logout();
            navManager.NavigateTo("/");
        }
    }
}