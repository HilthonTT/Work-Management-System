using WSMPortal.Models;
using UI.Library.Models;


namespace WSMPortal.Pages.Admin.Company
{
    public partial class CreateCompany
    {
        private CreateCompanyModel company = new();
        private List<UserModel> users;
        private string searchUserText = "";
        protected override async Task OnInitializedAsync()
        {
            company.DateFounded = DateTime.UtcNow;
            users = await userEndpoint.GetAllAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilterState();
                await SaveFilterState();
                StateHasChanged();
            }
        }

        private async Task LoadFilterState()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserText));
            searchUserText = stringResults.Success ? stringResults.Value : "";
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchUserText), searchUserText);
        }

        private async Task FilterUsers()
        {
            var output = await userEndpoint.GetAllAsync();
            if (string.IsNullOrWhiteSpace(searchUserText) == false)
            {
                output = output.Where(u => u.FirstName.Contains(searchUserText, StringComparison.InvariantCultureIgnoreCase) || u.LastName.Contains(searchUserText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            users = output;
            await SaveFilterState();
        }

        private async Task OnSearchInputUser(string searchInput)
        {
            searchUserText = searchInput;
            await FilterUsers();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task CreateCompanyAsync()
        {
            CompanyModel c = new();
            c.CompanyName = company.CompanyName;
            c.Address = company.Address;
            c.PhoneNumber = company.PhoneNumber;
            c.ChairPersonId = company.ChairPersonId;
            c.Description = company.Description;
            c.DateFounded = company.DateFounded;
            c.Archived = false;
            await companyEndpoint.PostCompanyAsync(c);
            company = new();
            ClosePage();
        }
    }
}