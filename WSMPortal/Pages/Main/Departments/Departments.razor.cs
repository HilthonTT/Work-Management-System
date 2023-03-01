using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Departments;

public partial class Departments
{
    private List<DepartmentModel> departments;
    private List<CompanyModel> companies;
    private bool isSortedByCreatedDate = false;
    private string selectedFilter = "Non-Archived";
    private string searchText = "";
    private int selectedCompany = 0;
    protected override async Task OnInitializedAsync()
    {
        await LoadDepartments();
        await LoadCompanies();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilterState();
            await FilterDepartments();
            StateHasChanged();
        }
    }

    private async Task LoadDepartments()
    {
        departments = null;

        string recordKey = "Departments_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        departments = await cache.GetRecordAsync<List<DepartmentModel>>(recordKey);

        if (departments is null)
        {
            departments = await departmentEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, departments);
        }
    }

    private async Task LoadCompanies()
    {
        companies = null;

        string recordKey = "Companies_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        companies = await cache.GetRecordAsync<List<CompanyModel>>(recordKey);

        if (companies is null)
        {
            companies = await companyEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, companies);
        }
    }

    private void OpenDetails(DepartmentModel department)
    {
        navManager.NavigateTo($"/departmentDetails/{department.Id}");
    }

    private async Task LoadFilterState()
    {
        var stringResults = await sessionStorage.GetAsync<string>(nameof(searchText));
        searchText = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedFilter));
        selectedFilter = stringResults.Success ? stringResults.Value : "Non-Archived";
        var intResults = await sessionStorage.GetAsync<int>(nameof(selectedCompany));
        selectedCompany = intResults.Success ? intResults.Value : 0;
        var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByCreatedDate));
        isSortedByCreatedDate = boolResults.Success ? boolResults.Value : true;
    }

    private async Task SaveFilterState()
    {
        await sessionStorage.SetAsync(nameof(searchText), searchText);
        await sessionStorage.SetAsync(nameof(selectedFilter), selectedFilter);
        await sessionStorage.SetAsync(nameof(selectedCompany), selectedCompany);
        await sessionStorage.SetAsync(nameof(isSortedByCreatedDate), isSortedByCreatedDate);
    }

    private async Task FilterDepartments()
    {
        await LoadDepartments();
        var output = departments;
        if (string.IsNullOrWhiteSpace(searchText) == false)
        {
            output = output.Where(d => d.DepartmentName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) || d.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (isSortedByCreatedDate)
        {
            output = output.OrderByDescending(d => d.CreatedDate).ToList();
        }
        else
        {
            output = output.OrderBy(d => d.CreatedDate).ToList();
        }

        if (selectedCompany != 0)
        {
            output = output.Where(d => d.CompanyId == selectedCompany).ToList();
        }

        if (selectedFilter == "Archived")
        {
            output = output.Where(d => d.Archived).ToList();
        }
        else if (selectedFilter == "Non-Archived")
        {
            output = output.Where(d => d.Archived == false).ToList();
        }

        departments = output;
        await SaveFilterState();
    }

    private async Task OrderByCreatedDate(bool isCreatedDate)
    {
        isSortedByCreatedDate = isCreatedDate;
        await FilterDepartments();
    }

    private async Task OrderByArchived(string filter = "All")
    {
        selectedFilter = filter;
        await FilterDepartments();
    }

    private async Task OnSearchInput(string searchInput)
    {
        searchText = searchInput;
        await FilterDepartments();
    }

    private async Task OnCompanyClick(int company = 0)
    {
        selectedCompany = company;
        await FilterDepartments();
    }

    private string GetSelectedCompany(int company = 0)
    {
        if (company == selectedCompany)
        {
            return "fw-bold";
        }

        return "";
    }

    private string SortedByCreatedDate(bool isCreatedDate)
    {
        if (isCreatedDate == isSortedByCreatedDate)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string SortedByArchived(string filter = "All")
    {
        if (filter == selectedFilter)
        {
            return "fw-bold";
        }

        return "";
    }
}