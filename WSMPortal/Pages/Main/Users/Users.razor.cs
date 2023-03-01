using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Users
{
    public partial class Users
    {
        private List<UserModel> users;
        private List<DepartmentModel> departments;
        private List<CompanyModel> companies;
        private List<JobTitleModel> jobs;
        private bool isSortedByCreatedDate = true;
        private int selectedDepartment = 0;
        private int selectedJob = 0;
        private string searchText = "";
        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            await LoadDepartments();
            await LoadJobTitles();
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

        private async Task LoadJobTitles()
        {
            jobs = null;

            string recordKey = "Jobs_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

            jobs = await cache.GetRecordAsync<List<JobTitleModel>>(recordKey);

            if (jobs is null)
            {
                jobs = await jobTitleEndpoint.GetAllAsync();

                await cache.SetRecordAsync(recordKey, jobs);
            }
        }

        private void OpenDetails(UserModel user)
        {
            navManager.NavigateTo($"/userDetails/{user.Id}");
        }

        private string GetDepartmentName(UserModel user)
        {
            var selectedDepartment = departments.Where(x => x.Id == user.DepartmentId).FirstOrDefault();
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
            var intResults = await sessionStorage.GetAsync<int>(nameof(selectedDepartment));
            selectedDepartment = intResults.Success ? intResults.Value : 0;
            intResults = await sessionStorage.GetAsync<int>(nameof(selectedJob));
            selectedJob = intResults.Success ? intResults.Value : 0;
            var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByCreatedDate));
            isSortedByCreatedDate = boolResults.Success ? boolResults.Value : true;
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchText), searchText);
            await sessionStorage.SetAsync(nameof(isSortedByCreatedDate), isSortedByCreatedDate);
            await sessionStorage.SetAsync(nameof(selectedDepartment), selectedDepartment);
            await sessionStorage.SetAsync(nameof(selectedJob), selectedJob);
        }

        private async Task FilterUsers()
        {
            await LoadUsers();
            var output = users;
            if (selectedDepartment != 0)
            {
                output = output.Where(c => c.DepartmentId == selectedDepartment).ToList();
            }

            if (selectedJob != 0)
            {
                output = output.Where(j => j.JobTitles.Where(x => x.Id == selectedJob).FirstOrDefault()?.Id == selectedJob).ToList();
            }

            if (string.IsNullOrWhiteSpace(searchText) == false)
            {
                output = output.Where(u => u.FirstName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) || u.LastName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (isSortedByCreatedDate)
            {
                output = output.OrderByDescending(u => u.CreatedDate).ToList();
            }
            else
            {
                output = output.OrderBy(u => u.CreatedDate).ToList();
            }

            users = output;
            await SaveFilterState();
        }

        private async Task OrderByDateCreated(bool isCreatedDate)
        {
            isSortedByCreatedDate = isCreatedDate;
            await FilterUsers();
        }

        private async Task OnSearchInput(string searchInput)
        {
            searchText = searchInput;
            await FilterUsers();
        }

        private string SortedByDateCreated(bool isCreatedDate)
        {
            if (isCreatedDate == isSortedByCreatedDate)
            {
                return "btn-success";
            }

            return "btn-danger";
        }

        private async Task OnDepartmentClick(int departmentId = 0)
        {
            selectedDepartment = departmentId;
            await FilterUsers();
        }

        private async Task OnJobTitleClick(int jobId = 0)
        {
            selectedJob = jobId;
            await FilterUsers();
        }

        private string GetSelectedDepartment(int department = 0)
        {
            if (department == selectedDepartment)
            {
                return "fw-bold";
            }

            return "";
        }

        private string GetSelectedJob(int job = 0)
        {
            if (job == selectedJob)
            {
                return "fw-bold";
            }

            return "";
        }
    }
}