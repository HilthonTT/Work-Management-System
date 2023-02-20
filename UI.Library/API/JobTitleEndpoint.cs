using Microsoft.Extensions.Logging;
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

    public async Task<List<JobTitleModel>> GetAllAsync()
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

    public async Task<JobTitleModel> GetByIdAsync(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/JobTitle/GetJobTitlesByName", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<JobTitleModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostJobTitleAsync(JobTitleModel JobTitle)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/JobTitle/Admin/InsertJobTitle", JobTitle);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Job Title of name ({JobName}) has successfully been added to the database", JobTitle.JobName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateJobTitleAsync(JobTitleModel JobTitle)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/JobTitle/Admin/UpdateJobTitle", JobTitle);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Job Title of Id ({Id}) has successfully been updated.", JobTitle.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveJobTitleAsync(JobTitleModel jobTitle)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/JobTitle/Admin/ArchiveJobTitle", jobTitle);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Job Title of Id ({Id}) has successfully been archived.", jobTitle.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
