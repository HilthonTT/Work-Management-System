using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    public List<TaskModel> GetTasks()
    {
        return _taskData.GetTasks();
    }

    [HttpPost]
    [Authorize]
    [Route("GetTasksByUserId")]
    public List<TaskModel> GetTasksByUserId(TaskModel task)
    {
        return _taskData.GetTaskByUserId(task);
    }

    [HttpPost]
    [Authorize]
    [Route("GetTaskById")]
    public TaskModel GetTaskById(TaskModel task)
    {
        return _taskData.GetTaskById(task);
    }

    [HttpGet]
    [Authorize]
    [Route("GetTasksByDepartmentId")]
    public List<TaskModel> GetTasksByDepartmentId(TaskModel task)
    {
        return _taskData.GetTaskByDepartmentId(task);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateTaskPercentage")]
    public void UpdateTaskPercentage(TaskModel task) 
    {
        _taskData.UpdatePercentage(task);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertTask")]
    public void InsertTask(TaskModel task)
    {
        _taskData.InsertTask(task);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateTask")]
    public void UpdateTask(TaskModel task) 
    {
        _taskData.UpdateTask(task);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveTask")]
    public void ArchiveTask(TaskModel task)
    {
        _taskData.ArchiveTask(task);
    }
}
