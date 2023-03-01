using UI.Library.Models;
using WSMPortal.Helpers;

namespace WSMPortal.Pages.Main.Items;

public partial class Items
{
    private List<ItemModel> items;
    private bool isSortedByPrice = true;
    private string selectedFilter = "Non-Archived";
    private string searchText = "";
    protected override async Task OnInitializedAsync()
    {
        await LoadItems();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilterState();
            await FilterItems();
            StateHasChanged();
        }
    }

    private async Task LoadItems()
    {
        items = null;

        string recordKey = "Items_" + DateTime.Now.ToString("ddMMyyyy_hhmm");

        items = await cache.GetRecordAsync<List<ItemModel>>(recordKey);

        if (items is null)
        {
            items = await itemEndpoint.GetAllAsync();
            
            await cache.SetRecordAsync(recordKey, items);
        }
    }

    private void OpenDetails(ItemModel item)
    {
        navManager.NavigateTo($"/itemDetails/{item.Id}");
    }
    
    private async Task LoadFilterState()
    {
        var stringResults = await sessionStorage.GetAsync<string>(nameof(searchText));
        searchText = stringResults.Success ? stringResults.Value : "";
        stringResults = await sessionStorage.GetAsync<string>(nameof(selectedFilter));
        selectedFilter = stringResults.Success ? stringResults.Value : "Non-Archived";
        var boolResults = await sessionStorage.GetAsync<bool>(nameof(isSortedByPrice));
        isSortedByPrice = boolResults.Success ? boolResults.Value : true;
    }

    private async Task SaveFilterState()
    {
        await sessionStorage.SetAsync(nameof(searchText), searchText);
        await sessionStorage.SetAsync(nameof(isSortedByPrice), isSortedByPrice);
        await sessionStorage.SetAsync(nameof(selectedFilter), selectedFilter);
    }

    private async Task FilterItems()
    {
        await LoadItems();
        var output = items;
        if (string.IsNullOrWhiteSpace(searchText) == false)
        {
            output = output.Where(i => i.ModelName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) || i.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (isSortedByPrice)
        {
            output = output.OrderByDescending(i => i.Price).ToList();
        }
        else
        {
            output = output.OrderByDescending(i => i.Quantity).ToList();
        }

        if (selectedFilter == "Archived")
        {
            output = output.Where(i => i.Archived).ToList();
        }
        else if (selectedFilter == "Non-Archived")
        {
            output = output.Where(i => i.Archived == false).ToList();
        }

        items = output;
        await SaveFilterState();
    }

    private async Task OrderByPrice(bool isPrice)
    {
        isSortedByPrice = isPrice;
        await FilterItems();
    }

    private async Task OrderByArchived(string filter = "All")
    {
        selectedFilter = filter;
        await FilterItems();
    }

    private async Task OnSearchInput(string searchInput)
    {
        searchText = searchInput;
        await FilterItems();
    }

    private string SortedByPrice(bool isPrice)
    {
        if (isPrice == isSortedByPrice)
        {
            return "btn-success";
        }

        return "btn-danger";
    }

    private string SortedByArchived(string filter = "All")
    {
        if (filter == selectedFilter)
        {
            return "fw-bold";
        }

        return "";
    }
}