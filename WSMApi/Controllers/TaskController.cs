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
    public List<TaskModel> Get()
    {
        return _taskData.GetTasks();
    }

    public record GettingUserId 
    (
        string UserId
    );

    [HttpPost]
    [Authorize]
    [Route("GetTasksByUserId")]
    public List<TaskModel> GetTasksByUserId(GettingUserId user)
    {
        return _taskData.GetTaskByUserId(user.UserId);
    }

    public record GettingId(
        int Id
        );

    [HttpPost]
    [Authorize]
    [Route("GetTaskById")]
    public List<TaskModel> GetTaskById(GettingId Id)
    {
        return _taskData.GetTaskById(Id.Id);
    }

    [HttpGet]
    [Authorize]
    [Route("GetMyTasks")]
    public List<TaskModel> GetMyTasks()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return _taskData.GetTaskByUserId(userId);
    }


    [HttpGet]
    [Authorize]
    [Route("GetTasksByDepartmentId")]
    public List<TaskModel> GetTasksByDepartmentId(int DepartmentId)
    {
        return _taskData.GetTaskByDepartmentId(DepartmentId);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("InsertTask")]
    public void InsertTask(TaskModel task)
    {
        _taskData.InsertTask(task);
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
    [Route("UpdateTask")]
    public void UpdateTask(TaskModel task) 
    {
        _taskData.UpdateTask(task);
    }
}
