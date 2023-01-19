using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    public async Task<List<CompanyModel>> GetAll()
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

    public async Task<List<CompanyModel>> GetByName(string CompanyName)
    {
        using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/GetCompanyByName", CompanyName))
        {
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
    }

    public async Task PostCompany(CompanyModel company)
    {
        using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/InsertCompany", company))
        {
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("The company has successfully been added to the database.");
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public async Task UpdateCompany(CompanyModel company)
    {
        using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Company/UpdateCompany", company))
        {
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("The company of Id ({Id}) has sucessfully been updated.", company.Id);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

}
