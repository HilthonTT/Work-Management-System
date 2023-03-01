using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Admin.Item
{
    public partial class CreateItem
    {
        private CreateItemModel item = new();
        private List<UserModel> users;
        private List<CompanyModel> companies;
        private string searchUserText = "";
        private string searchCompanyText = "";
        protected override async Task OnInitializedAsync()
        {
            users = await userEndpoint.GetAllAsync();
            companies = await companyEndpoint.GetAllAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilterState();
                await FilterUsers();
                await FilterCompanies();
                StateHasChanged();
            }
        }

        private async Task LoadFilterState()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserText));
            searchUserText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchCompanyText));
            searchCompanyText = stringResults.Success ? stringResults.Value : "";
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchUserText), searchUserText);
            await sessionStorage.SetAsync(nameof(searchCompanyText), searchCompanyText);
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

        private async Task FilterCompanies()
        {
            var output = await companyEndpoint.GetAllAsync();
            if (string.IsNullOrWhiteSpace(searchCompanyText) == false)
            {
                output = output.Where(c => c.CompanyName.Contains(searchCompanyText, StringComparison.InvariantCultureIgnoreCase) || c.Address.Contains(searchCompanyText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            companies = output;
            await SaveFilterState();
        }

        private async Task OnSearchInputUser(string searchInput)
        {
            searchUserText = searchInput;
            await FilterUsers();
        }

        private async Task OnSearchCompany(string searchInput)
        {
            searchCompanyText = searchInput;
            await FilterCompanies();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task CreateItemAsync()
        {
            ItemModel i = new();
            i.ModelName = item.ModelName;
            i.Description = item.Description;
            i.Quantity = item.Quantity;
            i.Price = item.Price;
            i.Location = item.Location;
            i.InternalSupplierPersonId = item.InternalSupplierPersonId;
            i.InternalSupplierCompanyId = item.InternalSupplierCompanyId;
            i.EAN = item.EAN;
            i.Archived = false;
            await itemEndpoint.InsertItemAsync(i);
            item = new();
            ClosePage();
        }
    }
}