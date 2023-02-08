using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using UI.Library.Models;

namespace UI.Library.API;

public class TaskEndpoint : ITaskEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<TaskEndpoint> _logger;

    public TaskEndpoint(IAPIHelper apiHelper,
                        ILogger<TaskEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<List<TaskModel>> GetAllAsync()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Task/GetTasks");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<TaskModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<List<TaskModel>> GetByUserIdAsync(string UserId)
    {
        var data = new
        {
            UserId = UserId
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/GetTasksByUserId", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<TaskModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<List<TaskModel>> GetByDepartmentIdAsync(string departmentId)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/GetTasksByDepartmentId", departmentId);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<TaskModel>>();

            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostTaskAsync(TaskModel task)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/InsertTask", task);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The task of title ({Title}) has successfully been added to the database", task.Title);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdatePercentageAsync(TaskModel task)
    {
        var data = new
        {
            Id = task.Id,
            PercentageDone = task.PercentageDone,
            IsDone = task.IsDone
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/UpdateTaskPercentage", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The task of Id ({Id}) has successfully been updated to the database", task.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateAsync(TaskModel task)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/UpdateTask", task);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The task of Id ({Id}) has successfully been updated to the database", task.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
