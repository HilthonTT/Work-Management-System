using Microsoft.AspNetCore.Components;
using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Main.Tasks
{
    public partial class ModifyPercentage
    {
        [Parameter]
        public int Id { get; set; }

        private TaskModel task;
        private ModifyPercentageTaskModel modifiedPercentage = new();
        private DepartmentModel department;
        protected override async Task OnInitializedAsync()
        {
            task = await taskEndpoint.GetTaskByIdAsync(Id);
            department = await departmentEndpoint.GetByIdAsync(task.Id);
            modifiedPercentage.PercentageDone = task.PercentageDone;
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task ModifyPercentageAsync()
        {
            task.PercentageDone = modifiedPercentage.PercentageDone;
            if (task.PercentageDone == 100)
            {
                task.IsDone = true;
            }
            else
            {
                task.IsDone = false;
            }

            await taskEndpoint.UpdatePercentageAsync(task);
            modifiedPercentage = new();
        }
    }
}