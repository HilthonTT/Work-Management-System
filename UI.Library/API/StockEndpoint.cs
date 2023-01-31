using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Library.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public async Task<List<MachineModel>> GetAllMachines()
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
    public async Task<List<MachineModel>> GetAllMachinesAdmin()
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

    public async Task<List<MachineModel>> GetMachineByModelName(string ModelName)
    {
        var data = new { ModelName };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetMachineByModelName", data);
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

    public async Task<List<MachineModel>> GetMachineById(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetMachineById", data);
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

    public async Task InsertMachine(MachineModel machine)
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

    public async Task UpdateMachine(MachineModel machine)
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

    public async Task DeleteMachine(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/DeleteMachine", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The machine of Id {Id} has been deleted from the database.", data.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    // Part section //

    public async Task<List<PartModel>> GetAllParts()
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
    public async Task<List<PartModel>> GetAllPartsAdmin()
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

    public async Task<List<PartModel>> GetPartByModelName(string ModelName)
    {
        var data = new { ModelName };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetPartByModelName", data);
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

    public async Task<List<PartModel>> GetPartById(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/GetPartById", data);
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

    public async Task InsertPart(PartModel part)
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

    public async Task UpdatePart(PartModel part)
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

    public async Task DeletePart(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Stock/Admin/DeletePart", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The part of Id {Id} has been deleted database", data.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
