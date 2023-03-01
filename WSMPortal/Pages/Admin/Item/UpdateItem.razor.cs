using Microsoft.AspNetCore.Components;
using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Admin.Item
{
    public partial class UpdateItem
    {
        [Parameter]
        public int Id { get; set; }

        private CreateItemModel updatedItem = new();
        private ItemModel item;
        private List<UserModel> users;
        private List<CompanyModel> companies;
        private string searchUserText = "";
        private string searchCompanyText = "";
        protected override async Task OnInitializedAsync()
        {
            item = await itemEndpoint.GetByIdAsync(Id);
            users = await userEndpoint.GetAllAsync();
            companies = await companyEndpoint.GetAllAsync();
            if (item is not null)
            {
                updatedItem.ModelName = item.ModelName;
                updatedItem.Description = item.Description;
                updatedItem.Quantity = item.Quantity;
                updatedItem.Price = item.Price;
                updatedItem.Location = item.Location;
                updatedItem.InternalSupplierPersonId = item.InternalSupplierPersonId;
                updatedItem.InternalSupplierCompanyId = item.InternalSupplierCompanyId;
                updatedItem.EAN = item.EAN;
                updatedItem.Archived = item.Archived;
            }
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

        private async Task UpdateItemAsync()
        {
            item.ModelName = updatedItem.ModelName;
            item.Description = updatedItem.Description;
            item.Quantity = updatedItem.Quantity;
            item.Price = updatedItem.Price;
            item.Location = updatedItem.Location;
            item.InternalSupplierPersonId = updatedItem.InternalSupplierPersonId;
            item.InternalSupplierCompanyId = updatedItem.InternalSupplierCompanyId;
            item.EAN = updatedItem.EAN;
            item.Archived = updatedItem.Archived;
            await itemEndpoint.UpdateItemAsync(item);
            updatedItem = new();
            ClosePage();
        }
    }
}