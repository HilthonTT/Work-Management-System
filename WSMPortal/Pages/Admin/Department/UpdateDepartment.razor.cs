using Microsoft.AspNetCore.Components;
using WSMPortal.Models;
using UI.Library.Models;


namespace WSMPortal.Pages.Admin.Department
{
    public partial class UpdateDepartment
    {
        [Parameter]
        public int Id { get; set; }

        private CreateDepartmentModel updatedDepartment = new();
        private DepartmentModel department;
        private List<UserModel> users;
        private List<CompanyModel> companies;
        private string searchUserText = "";
        private string searchCompanyText = "";
        protected override async Task OnInitializedAsync()
        {
            department = await departmentEndpoint.GetByIdAsync(Id);
            users = await userEndpoint.GetAllAsync();
            companies = await companyEndpoint.GetAllAsync();
            companies = companies.Where(x => x.Archived == false).ToList();
            if (department is not null)
            {
                updatedDepartment.CompanyId = department.CompanyId;
                updatedDepartment.DepartmentName = department.DepartmentName;
                updatedDepartment.Address = department.Address;
                updatedDepartment.PhoneNumber = department.PhoneNumber;
                updatedDepartment.Description = department.Description;
                updatedDepartment.CreatedDate = department.CreatedDate;
                updatedDepartment.Archived = department.Archived;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilterState();
                await FilterUsers();
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
            output = output.Where(x => x.Archived == false).ToList();
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

        private async Task OnSearchInputCompany(string searchInput)
        {
            searchCompanyText = searchInput;
            await FilterCompanies();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task UpdateDepartmentAsync()
        {
            department.CompanyId = updatedDepartment.CompanyId.Value;
            department.DepartmentName = updatedDepartment.DepartmentName;
            department.Address = updatedDepartment.Address;
            department.PhoneNumber = updatedDepartment.PhoneNumber;
            department.Description = updatedDepartment.Description;
            department.CreatedDate = updatedDepartment.CreatedDate;
            department.Archived = updatedDepartment.Archived;
            await departmentEndpoint.UpdateDepartmentAsync(department);
            updatedDepartment = new();
            ClosePage();
        }
    }
}