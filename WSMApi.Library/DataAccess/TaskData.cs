using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class TaskData : ITaskData
{
    private readonly ISqlDataAccess _sql;

    public TaskData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<TaskModel> GetTasks()
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetAll", new { }, "WSMData");

        return output;
    }

    public TaskModel GetTaskById(TaskModel task)
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetById", new { task.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public List<TaskModel> GetTaskByUserId(TaskModel task)
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetByUserId", new { task.UserId }, "WSMData");

        return output;
    }

    public List<TaskModel> GetTaskByDepartmentId(TaskModel task)
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetByDepartmentId", new { task.DepartmentId }, "WSMData");

        return output;
    }

    public void InsertTask(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_Insert", task, "WSMData");
    }

    public void UpdatePercentage(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_UpdatePercentage", new { task.Id, task.PercentageDone, task.IsDone }, "WSMData");
    }

    public void UpdateTask(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_Update", task, "WSMData");
    }

    public void ArchiveTask(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_Archive", new { task.Id, task.Archived }, "WSMData");
    }
}
