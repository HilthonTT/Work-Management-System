using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Tasks;

public partial class Index
{
    private List<TaskModel> tasks;
    private List<DepartmentModel> departments;

    private bool isSortedByNew = true;
    private bool isSortedByIsDone = false;
    private bool isSortedByArchived = false;
    private int selectedDepartment = 0;

    private string selectedUser = "";
    private string searchText = "";
    protected override async Task OnInitializedAsync()
    {
        await LoadTasks();
        await LoadDepartments();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilterState();
            await FilterTasks();
            StateHasChanged();
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

    private void LoadCreateReportPage()
    {
        navManager.NavigateTo("/createReport");
    }

    private void OpenDetails(TaskModel task)
    {
        navManager.NavigateTo($"/taskDetails/{task.Id}");
    }

    private string GetDepartmentName(TaskModel task)
    {
        var selectedDepartment = departments.Where(x => x.Id == task.DepartmentId).FirstOrDefault();
        if (selectedDepartment is not null)
        {
            return $"{selectedDepartment.DepartmentName} department";
        }

        return "No Department";
    }

    private async Task LoadFilterState()
    {
        var stringResults = await sessionStorage.GetAsync<string>(nameof(searchText));
        searchText = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedUser));
        selectedUser = stringResults.Success ? stringResults.Value : "";
        var intResults = await sessionStorage.GetAsync<int>(nameof(selectedDepartment));
        selectedDepartment = intResults.Success ? intResults.Value : 0;
        var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByNew));
        isSortedByNew = boolResults.Success ? boolResults.Value : true;
        boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByArchived));
        isSortedByArchived = boolResults.Success ? boolResults.Value : false;
        boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByIsDone));
        isSortedByIsDone = boolResults.Success ? boolResults.Value : false;
    }

    private async Task SaveFilterState()
    {
        await sessionStorage.SetAsync(nameof(searchText), searchText);
        await sessionStorage.SetAsync(nameof(selectedDepartment), selectedDepartment);
        await sessionStorage.SetAsync(nameof(isSortedByNew), isSortedByNew);
        await sessionStorage.SetAsync(nameof(isSortedByArchived), isSortedByArchived);
        await sessionStorage.SetAsync(nameof(isSortedByIsDone), isSortedByIsDone);
        await sessionStorage.SetAsync(nameof(selectedUser), selectedUser);
    }

    private async Task FilterTasks()
    {
        await LoadTasks();
        var output = tasks;
        if (selectedDepartment != 0)
        {
            output = output.Where(t => t.DepartmentId == selectedDepartment).ToList();
        }

        if (string.IsNullOrWhiteSpace(searchText) == false)
        {
            output = output.Where(t => t.Title.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (selectedUser != "")
        {
            output = output.Where(t => t.UserId == loggedInUser.Id).ToList();
        }

        if (isSortedByIsDone)
        {
            output = output.Where(t => t.IsDone).ToList();
        }
        else
        {
            output = output.Where(t => t.IsDone == false).ToList();
        }

        if (isSortedByNew)
        {
            output = output.OrderByDescending(t => t.DateCreated).ToList();
        }
        else
        {
            output = output.OrderByDescending(t => t.DateDue).ThenByDescending(t => t.DateCreated).ToList();
        }

        if (isSortedByArchived)
        {
            output = output.Where(x => x.Archived).ToList();
        }
        else
        {
            output = output.Where(x => x.Archived == false).ToList();
        }

        tasks = output;
        await SaveFilterState();
    }

    private async Task OrderByNew(bool isNew)
    {
        isSortedByNew = isNew;
        await FilterTasks();
    }

    private async Task OnSearchInput(string searchInput)
    {
        searchText = searchInput;
        await FilterTasks();
    }

    private async Task OnDepartmentClick(int departmentId = 0)
    {
        selectedDepartment = departmentId;
        await FilterTasks();
    }

    private async Task OnUserClick(string userId = "")
    {
        selectedUser = userId;
        await FilterTasks();
    }

    private async Task OrderByIsDone(bool isDone)
    {
        isSortedByIsDone = isDone;
        await FilterTasks();
    }

    private async Task ShowArchives(bool showArchived)
    {
        isSortedByArchived = showArchived;
        await FilterTasks();
    }

    private string GetSelectedDepartment(int department = 0)
    {
        if (department == selectedDepartment)
        {
            return "fw-bold";
        }
        else
        {
            return "";
        }
    }

    private string GetSelectedUser(string user = "")
    {
        if (user == selectedUser)
        {
            return "fw-bold";
        }
        else
        {
            return "";
        }
    }

    private string SortedByNewClass(bool isNew)
    {
        if (isNew == isSortedByNew)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string SortedByArchived(bool isArchived)
    {
        if (isArchived == isSortedByArchived)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string SortedByDone(bool isDone)
    {
        if (isDone == isSortedByIsDone)
        {
            return "btn-success";
        }

        return "btn-danger";
    }
}