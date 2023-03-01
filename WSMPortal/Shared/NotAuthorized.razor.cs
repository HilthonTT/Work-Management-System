namespace WSMPortal.Shared
{
    public partial class NotAuthorized
    {
        private void ClosePage()
        {
            navManager.NavigateTo("/");
        }
    }
}