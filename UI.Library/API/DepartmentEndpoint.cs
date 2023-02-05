using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<List<DepartmentModel>> GetAll()
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

    public async Task<List<DepartmentModel>> GetByName(string DepartmentName)
    {
        var data = new
        {
            DepartmentName
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/GetDepartmentByName", data);
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

    public async Task PostDepartment(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/InsertDepartment", department);
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

    public async Task UpdateDepartment(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/UpdateDepartment", department);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The department of Id ({Id}) has successfully been updated.", department.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task DeleteDepartment(DepartmentModel department)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Department/DeleteDepartment", department);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The department of Id {Id} has successfully been updated.", department.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
