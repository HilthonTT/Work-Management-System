using Microsoft.AspNetCore.Components;
using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Admin.Tasks
{
    public partial class UpdateTask
    {
        [Parameter]
        public int Id { get; set; }

        private UpdateTaskModel updatedTask = new();
        private TaskModel task;
        private List<DepartmentModel> departments;
        private List<UserModel> users;
        private string searchDepartmentText = "";
        private string searchUserText = "";
        protected override async Task OnInitializedAsync()
        {
            task = await taskEndpoint.GetTaskByIdAsync(Id);
            departments = await departmentEndpoint.GetAllAsync();
            users = await userEndpoint.GetAllAsync();
            if (task is not null)
            {
                updatedTask.Title = task.Title;
                updatedTask.UserId = task.UserId;
                updatedTask.DepartmentId = task.DepartmentId;
                updatedTask.Description = task.Description;
                updatedTask.DateDue = task.DateDue;
                updatedTask.PercentageDone = task.PercentageDone;
                updatedTask.IsDone = task.IsDone;
                updatedTask.DateCreated = task.DateCreated;
                updatedTask.Archived = task.Archived;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilterState();
                await FilterDepartments();
                await FilterUsers();
                StateHasChanged();
            }
        }

        private async Task LoadFilterState()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(searchDepartmentText));
            searchDepartmentText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserText));
            searchUserText = stringResults.Success ? stringResults.Value : "";
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchDepartmentText), searchDepartmentText);
            await sessionStorage.SetAsync(nameof(searchUserText), searchUserText);
        }

        private async Task FilterDepartments()
        {
            var output = await departmentEndpoint.GetAllAsync();
            if (string.IsNullOrWhiteSpace(searchDepartmentText) == false)
            {
                output = output.Where(d => d.DepartmentName.Contains(searchDepartmentText, StringComparison.InvariantCultureIgnoreCase) || d.Description.Contains(searchDepartmentText, StringComparison.InvariantCultureIgnoreCase) || d.Address.Contains(searchDepartmentText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            departments = output;
            await SaveFilterState();
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

        private async Task OnSearchInputDepartment(string searchInput)
        {
            searchDepartmentText = searchInput;
            await FilterDepartments();
        }

        private async Task OnSearchInputUser(string searchInput)
        {
            searchUserText = searchInput;
            await FilterUsers();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task UpdateTaskAsync()
        {
            task.Title = updatedTask.Title;
            task.UserId = updatedTask.UserId;
            task.DepartmentId = updatedTask.DepartmentId;
            task.Description = updatedTask.Description;
            task.DateDue = updatedTask.DateDue;
            task.PercentageDone = updatedTask.PercentageDone.Value;
            task.IsDone = updatedTask.IsDone;
            task.DateCreated = updatedTask.DateCreated;
            task.Archived = updatedTask.Archived;
            await taskEndpoint.UpdateAsync(task);
            updatedTask = new();
            ClosePage();
        }
    }
}