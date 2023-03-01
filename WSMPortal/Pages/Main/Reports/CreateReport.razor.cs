using WSMPortal.Models;
using UI.Library.Models;

namespace WSMPortal.Pages.Main.Reports
{
    public partial class CreateReport
    {
        private CreateReportModel report = new();
        private List<TaskModel> tasks;
        private string searchTaskText = "";
        protected override async Task OnInitializedAsync()
        {
            tasks = await taskEndpoint.GetAllAsync();
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

        private async Task LoadFilterState()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(searchTaskText));
            searchTaskText = stringResults.Success ? stringResults.Value : "";
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchTaskText), searchTaskText);
        }

        private async Task FilterTasks()
        {
            var output = await taskEndpoint.GetAllAsync();
            if (string.IsNullOrWhiteSpace(searchTaskText) == false)
            {
                output = output.Where(t => t.Title.Contains(searchTaskText, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(searchTaskText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            tasks = output;
            await SaveFilterState();
        }

        private async Task OnSearchInputTask(string searchInput)
        {
            searchTaskText = searchInput;
            await FilterTasks();
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task CreateReportAsync()
        {
            ReportModel r = new();
            r.TaskId = report.TaskId.Value;
            r.UserId = loggedInUser.Id;
            r.Title = report.Title;
            r.Description = report.Description;
            r.DateCreated = DateTime.UtcNow;
            r.Archived = false;
            await reportEndpoint.InsertReportAsync(r);
            report = new();
            ClosePage();
        }
    }
}