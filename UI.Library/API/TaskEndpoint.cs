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

    public async Task<TaskModel> GetTaskById(TaskModel task)
    {
        using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/GetTaskById", task))
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<TaskModel>();

                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public async Task<List<TaskModel>> GetByUserIdAsync(TaskModel task)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/GetTasksByUserId", task);
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

    public async Task<List<TaskModel>> GetByDepartmentIdAsync(TaskModel task)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/GetTasksByDepartmentId", task);
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
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/Admin/InsertTask", task);
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
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/UpdateTaskPercentage", task);
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
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/Admin/UpdateTask", task);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The task of Id ({Id}) has successfully been updated to the database", task.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ArchiveTask(TaskModel task)
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Task/Admin/ArchiveTask", task);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The task of Id ({Id}) has successfully been archived to the database", task.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
