using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Library.Models;

namespace UI.Library.API;

public class JobTitleEndpoint : IJobTitleEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<JobTitleEndpoint> _logger;

    public JobTitleEndpoint(IAPIHelper apiHelper, ILogger<JobTitleEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<List<JobTitleModel>> GetAll()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/JobTitle/GetJobTitles");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<JobTitleModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<List<JobTitleModel>> GetByName(string JobName)
    {
        var data = new
        {
            JobName
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/JobTitle/GetJobTitlesByName", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<JobTitleModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostJobTitle(JobTitleModel JobTitle)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/JobTitle/InsertJobTitle", JobTitle);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Job Title of name ({JobName}) has successfully been added to the database", JobTitle.JobName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateJobTitle(JobTitleModel JobTitle)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/JobTitle/UpdateJobTitle", JobTitle);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Job Title of Id ({Id}) has successfully been updated.", JobTitle.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
