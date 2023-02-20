using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IItemData
    {
        void ArchiveItem(ItemModel item);
        ItemModel GetItemById(ItemModel item);
        List<ItemModel> GetItems();
        void InsertItem(ItemModel item);
        void UpdateItem(ItemModel item);
    }
}