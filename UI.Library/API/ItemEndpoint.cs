using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UI.Library.Models;

namespace UI.Library.API;

public class ItemEndpoint : IItemEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<ItemEndpoint> _logger;

    public ItemEndpoint(IAPIHelper apiHelper,
                     ILogger<ItemEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger ?? NullLogger<ItemEndpoint>.Instance;
    }

    public async Task<List<ItemModel>> GetAllAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Item/GetItems");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<ItemModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<List<ItemModel>> GetAllAdminAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Item/Admin/GetItems");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<ItemModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<ItemModel> GetByIdAsync(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Item/GetItemById", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<ItemModel>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task InsertItemAsync(ItemModel item)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Item/Admin/InsertItem", item);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Item Of Model Name {ModelName} Has Been Added To The Database", item.ModelName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateItemAsync(ItemModel item)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/Item/Admin/UpdateItem", item);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Item Of Id {Id} Has Been Updated To The Database", item.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveItemAsync(ItemModel item)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/Item/Admin/ArchiveItem", item);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Item Of Id {Id} Has Been Archived To The Database", item.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
