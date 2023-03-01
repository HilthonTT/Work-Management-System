namespace WSMPortal.Pages.Admin
{
    public partial class AdminHub
    {
        private void GoToCreateCompany()
        {
            navManager.NavigateTo("/createCompany");
        }

        private void GoToCreateDepartment()
        {
            navManager.NavigateTo("/createDepartment");
        }

        private void GoToCreateTask()
        {
            navManager.NavigateTo("/createTask");
        }

        private void GoToCreateItem()
        {
            navManager.NavigateTo("/createItem");
        }

        private void ViewReports()
        {
            navManager.NavigateTo("/reports");
        }

        private void ModifyUserRoles()
        {
            navManager.NavigateTo("/userRoles");
        }
    }
}