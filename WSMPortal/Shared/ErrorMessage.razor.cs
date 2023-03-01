using WSMPortal.Helpers;

namespace WSMPortal.Shared
{
    public partial class ErrorMessage
    {
        private void ClosePage()
        {
            navManager.NavigateTo(PageHistoryState.GetGoBackPage(), forceLoad: true);
        }
    }
}