using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class ItemData : IItemData
{
    private readonly ISqlDataAccess _sql;

    public ItemData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<ItemModel> GetItems()
    {
        var output = _sql.LoadData<ItemModel, dynamic>("dbo.spItem_GetAll", new { }, "WSMData");

        return output;
    }

    public ItemModel GetItemById(ItemModel item)
    {
        var output = _sql.LoadData<ItemModel, dynamic>("dbo.spItem_GetById", new { item.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertItem(ItemModel item)
    {
        _sql.SaveData("dbo.spItem_Insert", item, "WSMData");
    }

    public void UpdateItem(ItemModel item)
    {
        _sql.SaveData("dbo.spItem_Update", item, "WSMData");
    }

    public void ArchiveItem(ItemModel item)
    {
        _sql.SaveData("dbo.spItem_Update", new { item.Id, item.Archived }, "WSMData");
    }
}
