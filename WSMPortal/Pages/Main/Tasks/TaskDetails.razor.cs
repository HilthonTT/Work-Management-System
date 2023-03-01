using Microsoft.AspNetCore.Components;
using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Tasks
{
    public partial class TaskDetails
    {
        [Parameter]
        public int Id { get; set; }

        private TaskModel task;
        private DepartmentModel department;
        protected override async Task OnInitializedAsync()
        {
            task = await taskEndpoint.GetTaskByIdAsync(Id);
            if (task is not null)
            {
                department = await departmentEndpoint.GetByIdAsync(task.Id);
            }

            PageHistoryState.AddPageToHistory($"/TaskDetails/{Id}");
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private void LoadModifyPercentagePage()
        {
            navManager.NavigateTo($"/modifyPercentage/{Id}");
        }

        private string GetDepartmentName()
        {
            if (department is not null)
            {
                return $"{department.DepartmentName} Was Assigned";
            }

            return "No Department Assigned";
        }

        private bool CanAccessModifyPage()
        {
            if (loggedInUser.DepartmentId == task.DepartmentId || loggedInUser.Id == task.UserId)
            {
                return true;
            }

            return false;
        }

        private void LoadUpdateTaskPage()
        {
            navManager.NavigateTo($"/updateTask/{Id}");
        }
    }
}