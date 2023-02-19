using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UI.Library.Models;

namespace UI.Library.API;

public class StockEndpoint : IStockEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger _logger;

    public StockEndpoint(IAPIHelper apiHelper,
                         ILogger logger)
    {
        _apiHelper = apiHelper;
        _logger = logger ?? NullLogger<StockEndpoint>.Instance;
    }

    // Machine section //

    public async Task<List<MachineModel>> GetAllMachinesAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Stock/GetMachines");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<MachineModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    // Added this method for the WPF AdminStockViewModel so only admin can only access this page
    // Gives out an error when ViewLoaded.
    public async Task<List<MachineModel>> GetAllMachinesAdminAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Stock/Admin/GetMachines");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<MachineModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<MachineModel> GetMachineByIdAsync(MachineModel machine)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetMachineById", machine);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<MachineModel>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task InsertMachineAsync(MachineModel machine)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/InsertMachine", machine);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The machine of ModelName {ModelName} has been added to the database", machine.ModelName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateMachineAsync(MachineModel machine)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/UpdateMachine", machine);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The machine of Id {MachineId} has been updated in the database", machine.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveMachineAsync(MachineModel machine)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/ArchiveMachine", machine);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The machine of Id {Id} has been archived in the database", machine.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    // Part section //

    public async Task<List<PartModel>> GetAllPartsAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Stock/GetParts");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<PartModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
    public async Task<List<PartModel>> GetAllPartsAdminAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Stock/Admin/GetParts");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<PartModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<PartModel> GetPartByIdAsync(PartModel part)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetPartByModelName", part);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<PartModel>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task InsertPartAsync(PartModel part)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/InsertPart", part);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The part of ModelName {ModelName} has been added the database", part.ModelName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdatePartAsync(PartModel part)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/UpdatePart", part);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The part of Id {Id} and of ModelName {ModelName} has been updated in the database", part.Id, part.ModelName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchivePartAsync(PartModel part)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/ArchivePart", part);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The part of Id {Id} has been archived database", part.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
