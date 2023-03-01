using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Authentication
{
    public partial class Register
    {
        CreateUserModel model = new();
        private string registrationErrorMessage = "";
        protected override void OnInitialized()
        {
            model.DateOfBirth = DateTime.UtcNow;
        }

        private async Task RegisterUser()
        {
            try
            {
                registrationErrorMessage = "";
                await userEndpoint.CreateUserAsync(model);
                AuthenticatedUserModel result = await authService.Login(new() { Email = model.EmailAddress, Password = model.Password });
                if (result is not null)
                {
                    navManager.NavigateTo("/");
                }
                else
                {
                    registrationErrorMessage = "You have been registered but there was an error when trying to log in.";
                }
            }
            catch (Exception ex)
            {
                registrationErrorMessage = ex.Message;
            }
        }
    }
}