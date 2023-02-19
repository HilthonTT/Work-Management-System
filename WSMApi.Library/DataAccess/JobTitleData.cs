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

    public JobTitleModel GetJobTitleById(JobTitleModel jobTitle)
    {
        var output = _sql.LoadData<JobTitleModel, dynamic>("dbo.spJobTitle_GetById", new { jobTitle.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertJobTitle(JobTitleModel jobTitle)
    {
        _sql.SaveData("dbo.spJobTitle_Insert", jobTitle, "WSMData");
    }

    public void UpdateJobTitle(JobTitleModel jobTitle)
    {
        _sql.SaveData("dbo.spJobTitle_Update", jobTitle, "WSMData");
    }

    public void ArchiveJobTitle(JobTitleModel jobTitle)
    {
        _sql.SaveData("dbo.spJobTitle_Archive", new { jobTitle.Id, jobTitle.Archived }, "WSMData");
    }
}
