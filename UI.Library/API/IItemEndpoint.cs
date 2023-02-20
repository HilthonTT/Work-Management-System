using UI.Library.Models;

namespace UI.Library.API
{
    public interface IItemEndpoint
    {
        Task ArchiveItemAsync(ItemModel item);
        Task<List<ItemModel>> GetAllAdminAsync();
        Task<List<ItemModel>> GetAllAsync();
        Task<ItemModel> GetByIdAsync(string Id);
        Task InsertItemAsync(ItemModel item);
        Task UpdateItemAsync(ItemModel item);
    }
}