using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskData _taskData;

    public TaskController(ITaskData taskData)
	{
        _taskData = taskData;
    }

    [HttpGet]
    [Authorize]
    [Route("GetTasks")]
    public List<TaskModel> Get()
    {
        return _taskData.GetTasks();
    }

    [HttpGet]
    [Authorize]
    [Route("GetTasksByUserId")]
    public List<TaskModel> GetTasksByUserId(string UserId)
    {
        return _taskData.GetTaskByUserId(UserId);
    }

    [HttpGet]
    [Authorize]
    [Route("GetTasksByDepartmentId")]
    public List<TaskModel> GetTasksByDepartmentId(int DepartmentId)
    {
        return _taskData.GetTaskByDepartmentId(DepartmentId);
    }

    [HttpPost]
    [Authorize]
    [Route("InsertTask")]
    public void InsertTask(TaskModel task)
    {
        _taskData.InsertTask(task);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateTaskPercentage")]
    public void UpdateTaskPercentage(int percentage) 
    {
        _taskData.UpdatePercentage(percentage);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateTask")]
    public void UpdateTask(TaskModel task) 
    {
        _taskData.UpdateTask(task);
    }
}
