using Microsoft.AspNetCore.Components;
using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Items
{
    public partial class ItemDetails
    {
        [Parameter]
        public int Id { get; set; }

        private ItemModel item;
        private List<UserModel> users;
        private List<CompanyModel> companies;
        protected override async Task OnInitializedAsync()
        {
            item = await itemEndpoint.GetByIdAsync(Id);
            if (item is not null)
            {
                users = await userEndpoint.GetAllAsync();
                companies = await companyEndpoint.GetAllAsync();
            }

            PageHistoryState.AddPageToHistory($"/ItemDetails/{Id}");
        }

        private string EanValue()
        {
            if (item.EAN is null || string.IsNullOrWhiteSpace(item.EAN.ToString()))
            {
                return "404";
            }

            return item.EAN.ToString();
        }

        private string GetInternalSupplierUserName()
        {
            var user = users.Where(u => u.Id == item.InternalSupplierPersonId).FirstOrDefault();
            if (user is not null)
            {
                return $"{user.FullName}";
            }

            return "No Supplier [Person]";
        }

        private string GetInternalSupplierCompany()
        {
            var company = companies.Where(c => c.Id == item.InternalSupplierCompanyId).FirstOrDefault();
            if (company is not null)
            {
                return $"{company.CompanyName}";
            }

            return "No Supplier [Company]";
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/items");
        }

        private void LoadUpdateItemPage()
        {
            navManager.NavigateTo($"/updateItem/{Id}");
        }
    }
}