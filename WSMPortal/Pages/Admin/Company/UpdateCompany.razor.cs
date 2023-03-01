using Microsoft.AspNetCore.Components;
using WSMPortal.Models;
using UI.Library.Models;


namespace WSMPortal.Pages.Admin.Company
{
    public partial class UpdateCompany
    {
        [Parameter]
        public int Id { get; set; }

        private CreateCompanyModel updatedCompany = new();
        private CompanyModel company;
        private List<UserModel> users;
        private string searchUserText = "";
        protected override async Task OnInitializedAsync()
        {
            company = await companyEndpoint.GetByIdAsync(Id);
            users = await userEndpoint.GetAllAsync();
            if (company is not null)
            {
                updatedCompany.CompanyName = company.CompanyName;
                updatedCompany.Address = company.Address;
                updatedCompany.PhoneNumber = company.PhoneNumber;
                updatedCompany.ChairPersonId = company.ChairPersonId;
                updatedCompany.Description = company.Description;
                updatedCompany.DateFounded = company.DateFounded;
                updatedCompany.Archived = company.Archived;
            }
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

        private async Task UpdateCompanyAsync()
        {
            company.CompanyName = updatedCompany.CompanyName;
            company.Address = updatedCompany.Address;
            company.PhoneNumber = updatedCompany.PhoneNumber;
            company.ChairPersonId = updatedCompany.ChairPersonId;
            company.Description = updatedCompany.Description;
            company.DateFounded = updatedCompany.DateFounded;
            company.Archived = updatedCompany.Archived;
            await companyEndpoint.UpdateCompanyAsync(company);
            updatedCompany = new();
            ClosePage();
        }
    }
}