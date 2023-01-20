﻿using System;
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

    public List<TaskModel> GetTaskByUserId(string UserId)
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetByUserId", new { UserId }, "WSMData");

        return output;
    }

    public List<TaskModel> GetTaskByDepartmentId(string DepartmentId)
    {
        var output = _sql.LoadData<TaskModel, dynamic>("dbo.spTask_GetByDepartmentId", new { DepartmentId }, "WSMData");

        return output;
    }

    public void InsertTask(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_Insert", task, "WSMData");
    }

    public void UpdatePercentage(int percentage)
    {
        _sql.SaveData("dbo.spTask_UpdatePercentage", new { percentage }, "WSMData");
    }

    public void UpdateTask(TaskModel task)
    {
        _sql.SaveData("dbo.spTask_Update", task, "WSMData");
    }
}
