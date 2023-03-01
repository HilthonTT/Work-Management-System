using Microsoft.AspNetCore.Components;
using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Departments
{
    public partial class DepartmentDetails
    {
        [Parameter]
        public int Id { get; set; }

        private DepartmentModel department;
        private List<UserModel> users;
        private List<CompanyModel> companies;
        protected override async Task OnInitializedAsync()
        {
            department = await departmentEndpoint.GetByIdAsync(Id);
            if (department is not null)
            {
                users = await userEndpoint.GetAllAsync();
                companies = await companyEndpoint.GetAllAsync();
            }

            PageHistoryState.AddPageToHistory($"/departmentDetails/{Id}");
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/departments");
        }

        private void LoadUpdateDepartmentPage()
        {
            navManager.NavigateTo($"/updateDepartment/{Id}");
        }

        private string GetCompanyName()
        {
            var company = companies.Where(c => c.Id == department.CompanyId).FirstOrDefault();
            if (company is not null)
            {
                return $"{company.CompanyName}";
            }

            return "No Company";
        }

        private string GetChairPersonUserName()
        {
            var user = users.Where(u => u.Id == department.ChairPersonId).FirstOrDefault();
            if (user is not null)
            {
                return $"{user.FullName}";
            }

            return "No User";
        }
    }
}