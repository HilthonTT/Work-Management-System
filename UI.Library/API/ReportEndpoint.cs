using Microsoft.Extensions.Logging;
using UI.Library.Models;

namespace UI.Library.API;

public class ReportEndpoint : IReportEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<ReportEndpoint> _logger;

    public ReportEndpoint(IAPIHelper apiHelper, ILogger<ReportEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<List<ReportModel>> GetAllAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Report/Admin/GetReports");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<ReportModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<ReportModel> GetByIdAsync(int Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Report/Admin/GetReportById", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<ReportModel>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task InsertReportAsync(ReportModel report)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Report/InsertReport", report);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Report Of Title {Title} Has Been Added To The Database", report.Title);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateReportAsync(ReportModel report)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/Report/UpdateReport", report);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Report Of Id {Id} Has Been Updated To The Database", report.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveReportAsync(ReportModel report)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/Report/ArchiveReport", report);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The Report Of Id {Id} Has Been Archived To The Database", report.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
