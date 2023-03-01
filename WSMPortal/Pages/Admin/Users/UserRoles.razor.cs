using UI.Library.Models;

namespace WSMPortal.Pages.Admin.Users
{
    public partial class UserRoles
    {
        private UserModel selectedUser = new();
        private List<UserModel> users;
        private List<string> availableRoles = new();
        private List<string> userRoles;
        private string selectedAvailableRole = "";
        private string selectedUserRole = "";
        private List<string> availableJobs = new();
        private List<string> userJobs = new();
        private string selectedAvailableJob = "";
        private string selectedUserJob = "";
        private string searchUserText = "";
        private string searchAvailableJobText = "";
        private string searchUserJobText = "";
        private string searchAvailableRoleText = "";
        private string searchUserRoleText = "";
        private string errorMessage = "";
        protected override async Task OnInitializedAsync()
        {
            users = await userEndpoint.GetAllAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadAllFilters();
                await SaveFilterState();
                StateHasChanged();
            }
        }

        private async Task LoadFilterState()
        {
            var stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserText));
            searchUserText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchAvailableJobText));
            searchAvailableJobText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchAvailableRoleText));
            searchAvailableRoleText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserJobText));
            searchUserJobText = stringResults.Success ? stringResults.Value : "";
            stringResults = await sessionStorage.GetAsync<string>(nameof(searchUserRoleText));
            searchUserRoleText = stringResults.Success ? stringResults.Value : "";
        }

        private async Task SaveFilterState()
        {
            await sessionStorage.SetAsync(nameof(searchUserText), searchUserText);
            await sessionStorage.SetAsync(nameof(searchAvailableJobText), searchAvailableJobText);
            await sessionStorage.SetAsync(nameof(searchAvailableRoleText), searchAvailableRoleText);
            await sessionStorage.SetAsync(nameof(searchUserJobText), searchUserJobText);
            await sessionStorage.SetAsync(nameof(searchUserRoleText), searchUserRoleText);
        }

        private async Task LoadAllFilters()
        {
            await FilterUsers();
            await FilterUserRoles();
            await FilterAvailableRoles();
            await FilterUserJobs();
            await FilterAvailableJobs();
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

        private async Task FilterAvailableJobs()
        {
            await LoadJobs();
            var output = availableJobs;
            if (string.IsNullOrWhiteSpace(searchAvailableJobText) == false)
            {
                output = output.Where(j => j.Contains(searchAvailableJobText, StringComparison.InvariantCultureIgnoreCase) || j.Contains(searchAvailableJobText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            availableJobs = output;
            await SaveFilterState();
        }

        private async Task FilterUserJobs()
        {
            var userJobList = selectedUser.JobTitles.Select(j => j.JobName).ToList();
            var output = userJobList;
            if (string.IsNullOrWhiteSpace(searchUserJobText) == false)
            {
                output = output.Where(j => j.Contains(searchUserJobText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            userJobs = output;
            await SaveFilterState();
        }

        private async Task FilterAvailableRoles()
        {
            await LoadRoles();
            var output = availableRoles;
            if (string.IsNullOrWhiteSpace(searchAvailableRoleText) == false)
            {
                output = output.Where(r => r.Contains(searchAvailableRoleText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            availableRoles = output;
            await SaveFilterState();
        }

        private async Task FilterUserRoles()
        {
            var userRolesList = new List<string>();
            var allUserRoles = selectedUser.Roles;
            foreach (var r in allUserRoles)
            {
                userRolesList.Add(r.Value);
            }

            var output = userRolesList;
            if (string.IsNullOrWhiteSpace(searchUserRoleText) == false)
            {
                output = output.Where(r => r.Contains(searchUserRoleText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            userRoles = output;
            await SaveFilterState();
        }

        private async Task OnSearchInputUser(string searchInput)
        {
            searchUserText = searchInput;
            await FilterUsers();
        }

        private async Task OnSearchInputAvailableRole(string searchInput)
        {
            searchAvailableRoleText = searchInput;
            await FilterAvailableRoles();
        }

        private async Task OnSearchInputUserRole(string searchInput)
        {
            searchUserRoleText = searchInput;
            await FilterUserRoles();
        }

        private async Task OnSearchInputAvailableJob(string searchInput)
        {
            searchAvailableJobText = searchInput;
            await FilterAvailableJobs();
        }

        private async Task OnSearchInputUserJob(string searchInput)
        {
            searchUserJobText = searchInput;
            await FilterUserJobs();
        }

        private async Task LoadRoles()
        {
            var roles = await userEndpoint.GetAllRolesAsync();
            availableRoles.Clear();
            foreach (var r in roles)
            {
                if (userRoles.IndexOf(r.Value) < 0)
                {
                    availableRoles.Add(r.Value);
                }
            }
        }

        private async Task LoadJobs()
        {
            var jobs = await jobTitleEndpoint.GetAllAsync();
            availableJobs.Clear();
            foreach (var j in jobs)
            {
                if (userJobs.IndexOf(j.JobName) < 0)
                {
                    availableJobs.Add(j.JobName);
                }
            }
        }

        private async Task AddSelectedRole()
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedAvailableRole))
            {
                errorMessage = "You didn't select a user or an available role to add.";
                return;
            }

            await userEndpoint.AddUserToRoleAsync(selectedUser.Id, selectedAvailableRole);
            userRoles.Add(selectedAvailableRole);
            availableRoles.Remove(selectedAvailableRole);
            selectedAvailableRole = "";
        }

        private async Task RemoveSelectedRole()
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedUserRole))
            {
                errorMessage = "You didn't select a user or a user role to remove.";
                return;
            }

            await userEndpoint.RemoveUserFromRoleAsync(selectedUser.Id, selectedUserRole);
            availableRoles.Add(selectedUserRole);
            userRoles.Remove(selectedUserRole);
            selectedUserRole = "";
        }

        private async Task AddSelectedJob()
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedAvailableJob))
            {
                errorMessage = "You didn't select a user or an available Job to add.";
                return;
            }

            var user = await userEndpoint.GetByIdAsync(selectedUser.Id);
            var jobs = await jobTitleEndpoint.GetAllAsync();
            var chosenJob = jobs.Where(j => j.JobName == selectedAvailableJob).FirstOrDefault();
            var selectedJob = await jobTitleEndpoint.GetByIdAsync(chosenJob.Id);
            user.JobTitleId = chosenJob.Id;
            await userEndpoint.UpdateUserJobTitleIdAsync(user);
            userJobs.Add(selectedJob.JobName);
            availableJobs.Remove(selectedJob.JobName);
            selectedAvailableJob = "";
        }

        private async Task RemoveSelectedJob()
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedUserJob))
            {
                errorMessage = "You didn't select a user or a user Job to remove.";
                return;
            }

            var user = await userEndpoint.GetByIdAsync(selectedUser.Id);
            var jobs = await jobTitleEndpoint.GetAllAsync();
            var chosenJob = jobs.Where(j => j.JobName == selectedUserJob).FirstOrDefault();
            var selectedJob = await jobTitleEndpoint.GetByIdAsync(chosenJob.Id);
            user.JobTitleId = null;
            await userEndpoint.UpdateUserJobTitleIdAsync(user);
            userJobs.Remove(selectedJob.JobName);
            availableJobs.Add(selectedJob.JobName);
            selectedUserJob = "";
        }

        private async Task LoadSelectedUser(string Id)
        {
            selectedAvailableRole = "";
            selectedAvailableJob = "";
            selectedUserRole = "";
            selectedUserJob = "";
            selectedUser = await userEndpoint.GetByIdAsync(Id);
            userRoles = new List<string>(selectedUser.Roles.Select(x => x.Value));
            await LoadRoles();
            userJobs = new List<string>(selectedUser.JobTitles.Select(j => j.JobName));
            await LoadJobs();
        }

        private void LoadSelectedAvailableRole(string roleName)
        {
            selectedAvailableRole = roleName;
        }

        private void LoadSelectedUserRole(string roleName)
        {
            selectedUserRole = roleName;
        }

        private void LoadSelectedAvailableJob(string job)
        {
            selectedAvailableJob = job;
        }

        private void LoadSelectedUserJob(string job)
        {
            selectedUserJob = job;
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }

        private async Task<string> GetJobName(int Id)
        {
            var job = await jobTitleEndpoint.GetByIdAsync(Id);
            if (job is not null)
            {
                return job.JobName;
            }

            return "N/A";
        }

        private string AddSelectedRoleClass()
        {
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedAvailableRole))
            {
                return "btn-danger";
            }

            return "btn-success";
        }

        private string RemoveSelectedRoleClass()
        {
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedUserRole))
            {
                return "btn-danger";
            }

            return "btn-success";
        }

        private string AddSelectedJobClass()
        {
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedAvailableJob))
            {
                return "btn-danger";
            }

            return "btn-success";
        }

        private string RemoveSelectedJobClass()
        {
            if (string.IsNullOrWhiteSpace(selectedUser.Id) || string.IsNullOrWhiteSpace(selectedUserJob))
            {
                return "btn-danger";
            }

            return "btn-success";
        }
    }
}