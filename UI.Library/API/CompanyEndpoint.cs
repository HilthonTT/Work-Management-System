using Microsoft.Extensions.Logging;
using UI.Library.Models;

namespace UI.Library.API;

public class CompanyEndpoint : ICompanyEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<CompanyEndpoint> _logger;

    public CompanyEndpoint(IAPIHelper apiHelper,
                           ILogger<CompanyEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<List<CompanyModel>> GetAllAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Company/GetCompanies");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<CompanyModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<CompanyModel> GetByIdAsync(CompanyModel company)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/GetCompanyById", company);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<CompanyModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostCompanyAsync(CompanyModel company)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/Admin/InsertCompany", company);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The company of name ({CompanyName}) has successfully been added to the database.", company.CompanyName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateCompanyAsync(CompanyModel company)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/Admin/UpdateCompany", company);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The company of Id ({Id}) has sucessfully been updated.", company.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveCompanyAsync(CompanyModel company)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/Admin/ArchiveCompany", company);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The company of Id ({Id}) has sucessfully been archived.", company.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
