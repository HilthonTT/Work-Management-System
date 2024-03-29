using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Companies;

public partial class Companies
{
    private List<CompanyModel> companies;
    private List<UserModel> users;
    private bool isSortedByDateFounded = false;
    private string selectedFilter = "Non-Archived";
    private string searchText = "";
    private string selectedUser = "";
    protected override async Task OnInitializedAsync()
    {
        await LoadCompanies();
        await LoadUsers();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilterState();
            await FilterCompanies();
            StateHasChanged();
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

    private async Task LoadUsers()
    {
        users = null;

        string recordKey = "Users_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        users = await cache.GetRecordAsync<List<UserModel>>(recordKey);

        if (users is null)
        {
            users = await userEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, users);
        }
    }

    private void OpenDetails(CompanyModel company)
    {
        navManager.NavigateTo($"/companyDetails/{company.Id}");
    }

    private async Task LoadFilterState()
    {
        var stringResults = await sessionStorage.GetAsync<string>(nameof(searchText));
        searchText = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedFilter));
        selectedFilter = stringResults.Success ? stringResults.Value : "Non-Archived";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedUser));
        selectedUser = stringResults.Success ? stringResults.Value : "";
        var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByDateFounded));
        isSortedByDateFounded = boolResults.Success ? boolResults.Value : true;
    }

    private async Task SaveFilterState()
    {
        await sessionStorage.SetAsync(nameof(searchText), searchText);
        await sessionStorage.SetAsync(nameof(selectedFilter), selectedFilter);
        await sessionStorage.SetAsync(nameof(isSortedByDateFounded), isSortedByDateFounded);
    }

    private async Task FilterCompanies()
    {
        await LoadCompanies();
        var output = companies;
        if (string.IsNullOrWhiteSpace(searchText) == false)
        {
            output = output.Where(d => d.CompanyName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) || d.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (isSortedByDateFounded)
        {
            output = output.OrderByDescending(c => c.DateFounded).ToList();
        }
        else
        {
            output = output.OrderBy(c => c.DateFounded).ToList();
        }

        if (string.IsNullOrWhiteSpace(selectedUser) == false)
        {
            output = output.Where(c => c.ChairPersonId == selectedUser).ToList();
        }

        if (selectedFilter == "Archived")
        {
            output = output.Where(c => c.Archived).ToList();
        }
        else if (selectedFilter == "Non-Archived")
        {
            output = output.Where(c => c.Archived == false).ToList();
        }

        companies = output;
        await SaveFilterState();
    }

    private async Task OrderByDateFounded(bool isDateFounded)
    {
        isSortedByDateFounded = isDateFounded;
        await FilterCompanies();
    }

    private async Task OrderByArchived(string filter = "All")
    {
        selectedFilter = filter;
        await FilterCompanies();
    }

    private async Task OnSearchInput(string searchInput)
    {
        searchText = searchInput;
        await FilterCompanies();
    }

    private async Task OnUserClick(string user = "")
    {
        selectedUser = user;
        await FilterCompanies();
    }

    private string GetSelectedUser(string user = "")
    {
        if (user == selectedUser)
        {
            return "fw-bold";
        }

        return "";
    }

    private string SortedByArchived(string filter = "All")
    {
        if (filter == selectedFilter)
        {
            return "fw-bold";
        }

        return "";
    }

    private string SortedByDateFounded(bool isDateFounded)
    {
        if (isDateFounded == isSortedByDateFounded)
        {
            return "btn-success";
        }

        return "btn-danger";
    }
}