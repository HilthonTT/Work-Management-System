using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemData _itemData;

    public ItemController(IItemData itemData)
	{
        _itemData = itemData;
    }

    [HttpGet]
    [Authorize]
    [Route("GetItems")]
    public List<ItemModel> GetItems()
    {
        return _itemData.GetItems();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("Admin/GetItems")]
    public List<ItemModel> GetItemsAdmin()
    {
        return _itemData.GetItems();
    }

    [HttpPost]
    [Authorize]
    [Route("GetItemById")]
    public ItemModel GetItemById(ItemModel item)
    {
        return _itemData.GetItemById(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertItem")]
    public void InsertItem(ItemModel item)
    {
        _itemData.InsertItem(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateItem")]
    public void UpdateItem(ItemModel item)
    {
        _itemData.UpdateItem(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveItem")]
    public void ArchiveItem(ItemModel item)
    {
        _itemData.ArchiveItem(item);
    }
}
