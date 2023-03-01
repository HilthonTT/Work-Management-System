using Microsoft.AspNetCore.Components;
using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Admin.Reports
{
    public partial class ReportDetails
    {
        [Parameter]
        public int Id { get; set; }

        private ReportModel report;
        private UserModel user;
        private TaskModel task;
        protected override async Task OnInitializedAsync()
        {
            report = await reportEndpoint.GetByIdAsync(Id);
            if (report is not null)
            {
                user = await userEndpoint.GetByIdAsync(report.UserId);
                task = await taskEndpoint.GetTaskByIdAsync(report.TaskId);
            }

            PageHistoryState.AddPageToHistory($"/reportDetails/{Id}");
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private void LoadTaskDetailsPage()
        {
            navManager.NavigateTo($"/taskDetails/{task.Id}");
        }

        private void LoadUserDetailsPage()
        {
            navManager.NavigateTo($"/userDetails/{user.Id}");
        }

        private string GetUserName()
        {
            if (user is not null)
            {
                return user.FullName;
            }

            return "N/A";
        }

        private string GetTaskTitle()
        {
            if (task is not null)
            {
                return task.Title;
            }

            return "N/A";
        }
    }
}