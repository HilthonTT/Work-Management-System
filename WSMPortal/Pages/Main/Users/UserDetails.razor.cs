using Microsoft.AspNetCore.Components;
using UI.Library.Models;

namespace WSMPortal.Pages.Main.Users
{
    public partial class UserDetails
    {
        [Parameter]
        public string Id { get; set; }

        private UserModel user;
        private DepartmentModel department;
        private JobTitleModel job;
        protected override async Task OnInitializedAsync()
        {
            user = await userEndpoint.GetByIdAsync(Id);
            if (user is not null && user.DepartmentId is not null)
            {
                department = await departmentEndpoint.GetByIdAsync(user.DepartmentId.Value);
            }

            if (user is not null && user.JobTitleId is not null)
            {
                job = await jobEndpoint.GetByIdAsync(user.JobTitleId.Value);
            }
        }

        private void ClosePage()
        {
            navManager.NavigateTo("/users");
        }

        private string GetDepartmentName()
        {
            if (department is not null)
            {
                return department.DepartmentName;
            }

            return "N/A";
        }

        private string GetJobName()
        {
            if (job is not null)
            {
                return job.JobName;
            }

            return "N/A";
        }
    }
}