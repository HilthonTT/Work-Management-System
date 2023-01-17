using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class JobTitleData : IJobTitleData
{
    private readonly ISqlDataAccess _sql;

    public JobTitleData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<JobTitleModel> GetJobTitles()
    {
        var output = _sql.LoadData<JobTitleModel, dynamic>("dbo.spJobTitle_GetAll", new { }, "WSMData");

        return output;
    }

    public List<JobTitleModel> GetJobTitleByName(string JobName)
    {
        var output = _sql.LoadData<JobTitleModel, dynamic>("dbo.spJobTitle_GetByName", new { JobName }, "WSMData");

        return output;
    }

    public void InsertJobTitle(JobTitleModel jobTitle)
    {
        _sql.SaveData("dbo.spJobTitle_Insert", jobTitle, "WSMData");
    }

    public void UpdateJobTitle(JobTitleModel jobTitle)
    {
        _sql.SaveData("dbo.spJobTitle_Update", jobTitle, "WSMData");
    }
}
