using Microsoft.Extensions.Logging;
using UI.Library.Models;

namespace UI.Library.API;

public class DepartmentEndpoint : IDepartmentEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<DepartmentEndpoint> _logger;

    public DepartmentEndpoint(IAPIHelper apiHelper,
                              ILogger<DepartmentEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<List<DepartmentModel>> GetAllAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Department/GetDepartments");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<DepartmentModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<DepartmentModel> GetByIdAsync(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/GetDepartmentById", department);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<DepartmentModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostDepartmentAsync(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/Admin/InsertDepartment", department);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The department of name ({DepartmentName}) has successfully been added to the database.",
                department.DepartmentName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateDepartmentAsync(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/Admin/UpdateDepartment", department);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The department of Id ({Id}) has successfully been updated.", department.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveDepartmentAsync(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/Admin/ArchiveDepartment", department);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The department of Id {Id} has successfully been archived.", department.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
