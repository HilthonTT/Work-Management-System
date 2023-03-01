using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Admin.Reports;

public partial class Reports
{
    private List<ReportModel> reports;
    private List<TaskModel> tasks;
    private List<UserModel> users;
    private List<TaskModel> searchTasks;
    private List<UserModel> searchUsers;
    private bool isSortedByNew = true;
    private bool isSortedByArchived = false;
    private string selectedUser = "";
    private int selectedTask = 0;
    private string searchTextReport = "";
    private string searchTextTask = "";
    private string searchTextUser = "";
    protected override async Task OnInitializedAsync()
    {
        await LoadReports();
        await LoadTasks();
        await LoadUsers();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilterState();
            await FilterReports();
            await FilterUsers();
            await FilterTasks();
            StateHasChanged();
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

    private async Task LoadReports()
    {
        reports = null;

        string recordKey = "Reports_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        reports = await cache.GetRecordAsync<List<ReportModel>>(recordKey);

        if (reports is null)
        {
            reports = await reportEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, reports);
        }
    }
    
    private async Task LoadTasks()
    {
        tasks = null;

        string recordKey = "Tasks_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        tasks = await cache.GetRecordAsync<List<TaskModel>>(recordKey);

        if (tasks is null)
        {
            tasks = await taskEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, tasks);
        }
    }

    private async Task LoadSearchTasks()
    {
        searchTasks = null;

        string recordKey = "SearchTasks_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        searchTasks = await cache.GetRecordAsync<List<TaskModel>>(recordKey);

        if (searchTasks is null)
        {
            searchTasks = await taskEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, searchTasks);
        }
    }

    private async Task LoadSearchUsers()
    {
        searchUsers = null;

        string recordKey = "SearchUsers_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        searchUsers = await cache.GetRecordAsync<List<UserModel>>(recordKey);

        if (searchUsers is null)
        {
            searchUsers = await userEndpoint.GetAllAsync();

            await cache.SetRecordAsync(recordKey, searchUsers);
        }
    }


    private async Task LoadFilterState()
    {
        var stringResults = await sessionStorage.GetAsync<string>(nameof(searchTextReport));
        searchTextReport = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(searchTextTask));
        searchTextTask = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(searchTextUser));
        searchTextUser = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedUser));
        selectedUser = stringResults.Success ? stringResults.Value : "";
        var intResults = await sessionStorage.GetAsync<int>(nameof(selectedTask));
        selectedTask = intResults.Success ? intResults.Value : 0;
        var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByNew));
        isSortedByNew = boolResults.Success ? boolResults.Value : true;
        boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByArchived));
        isSortedByArchived = boolResults.Success ? boolResults.Value : false;
    }

    private async Task SaveFilterState()
    {
        await sessionStorage.SetAsync(nameof(searchTextReport), searchTextReport);
        await sessionStorage.SetAsync(nameof(searchTextTask), searchTextTask);
        await sessionStorage.SetAsync(nameof(searchTextUser), searchTextUser);
        await sessionStorage.SetAsync(nameof(selectedUser), selectedUser);
        await sessionStorage.SetAsync(nameof(selectedTask), selectedTask);
        await sessionStorage.SetAsync(nameof(isSortedByNew), isSortedByNew);
        await sessionStorage.SetAsync(nameof(isSortedByArchived), isSortedByArchived);
    }

    private async Task FilterReports()
    {
        await LoadReports();
        var output = reports;
        if (string.IsNullOrWhiteSpace(selectedUser) == false)
        {
            output = output.Where(r => r.UserId == selectedUser).ToList();
        }

        if (selectedTask != 0)
        {
            output = output.Where(r => r.TaskId == selectedTask).ToList();
        }

        if (string.IsNullOrWhiteSpace(searchTextReport) == false)
        {
            output = output.Where(r => r.Title.Contains(searchTextReport, StringComparison.InvariantCultureIgnoreCase) || r.Description.Contains(searchTextReport, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (isSortedByNew)
        {
            output = output.OrderByDescending(r => r.DateCreated).ToList();
        }
        else
        {
            output = output.OrderBy(r => r.DateCreated).ToList();
        }

        if (isSortedByArchived)
        {
            output = output.Where(r => r.Archived).ToList();
        }
        else
        {
            output = output.Where(r => r.Archived == false).ToList();
        }

        reports = output;
        await SaveFilterState();
    }

    private async Task FilterUsers()
    {
        await LoadSearchUsers();
        var output = searchUsers;
        if (string.IsNullOrWhiteSpace(searchTextUser) == false)
        {
            output = output.Where(u => u.FirstName.Contains(searchTextUser, StringComparison.InvariantCultureIgnoreCase) || u.LastName.Contains(searchTextUser, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        users = output;
        await SaveFilterState();
    }

    private async Task FilterTasks()
    {
        await LoadSearchTasks();
        var output = searchTasks;
        if (string.IsNullOrWhiteSpace(searchTextTask) == false)
        {
            output = output.Where(t => t.Title.Contains(searchTextTask, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(searchTextTask, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        tasks = output;
        await SaveFilterState();
    }

    private async Task OnSearchInputReport(string searchInput)
    {
        searchTextReport = searchInput;
        await FilterReports();
    }

    private async Task OnSearchInputUser(string searchInput)
    {
        searchTextUser = searchInput;
        await FilterUsers();
    }

    private async Task OnSearchInputTask(string searchInput)
    {
        searchTextTask = searchInput;
        await FilterTasks();
    }

    private async Task OrderByNew(bool isNew)
    {
        isSortedByNew = isNew;
        await FilterReports();
    }

    private async Task ShowArchives(bool showArchives)
    {
        isSortedByArchived = showArchives;
        await FilterReports();
    }

    private async Task OnUserClick(string userId = "")
    {
        selectedUser = userId;
        await FilterReports();
    }

    private async Task OnTaskClick(int taskId = 0)
    {
        selectedTask = taskId;
        await FilterReports();
    }

    private string SortedByNewClass(bool isNew)
    {
        if (isNew == isSortedByNew)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string SortedByArchivedClass(bool isArchived)
    {
        if (isArchived == isSortedByArchived)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string GetSelectedUser(string userId = "")
    {
        if (userId == selectedUser)
        {
            return "fw-bold";
        }

        return "";
    }

    private string GetSelectedTask(int taskId = 0)
    {
        if (taskId == selectedTask)
        {
            return "fw-bold";
        }

        return "";
    }

    private string GetTaskTitle(ReportModel report)
    {
        var task = tasks.Where(t => t.Id == report.TaskId).FirstOrDefault();
        if (task is not null)
        {
            return task.Title;
        }

        return "N/A";
    }

    private string GetTaskUser(ReportModel report)
    {
        var user = users.Where(u => u.Id == report.UserId).FirstOrDefault();
        if (user is not null)
        {
            return user.FullName;
        }

        return "N/A";
    }

    private void OpenDetails(ReportModel report)
    {
        navManager.NavigateTo($"/reportDetails/{report.Id}");
    }
}