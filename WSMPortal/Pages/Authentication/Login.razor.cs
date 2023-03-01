using WSMPortal.Models;

namespace WSMPortal.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationUserModel model = new();
        private string authenticationErrorMessage = "";
        private async Task ExecuteLogin()
        {
            authenticationErrorMessage = "";
            AuthenticatedUserModel result = await authService.Login(model);
            if (result is not null)
            {
                navManager.NavigateTo("/");
            }
            else
            {
                authenticationErrorMessage = "There was error when trying to login.";
            }
        }

        private void LoadRegistrationPage()
        {
            navManager.NavigateTo("/register");
        }
    }
}