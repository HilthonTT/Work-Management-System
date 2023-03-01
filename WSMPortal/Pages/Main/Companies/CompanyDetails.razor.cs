using Microsoft.AspNetCore.Components;
using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Companies
{
    public partial class CompanyDetails
    {
        [Parameter]
        public int Id { get; set; }

        private CompanyModel company;
        private List<UserModel> users;
        protected override async Task OnInitializedAsync()
        {
            company = await companyEndpoint.GetByIdAsync(Id);
            if (company is not null)
            {
                users = await userEndpoint.GetAllAsync();
            }

            PageHistoryState.AddPageToHistory($"/companyDetails/{Id}");
        }

        private string GetChairpersonUserName()
        {
            var user = users.Where(u => u.Id == company.ChairPersonId).FirstOrDefault();
            if (user is not null)
            {
                return $"{user.FullName}";
            }

            return "No Chair Person";
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/companies");
        }

        private void LoadUpdateCompanyPage()
        {
            navManager.NavigateTo($"/updateCompany/{Id}");
        }
    }
}