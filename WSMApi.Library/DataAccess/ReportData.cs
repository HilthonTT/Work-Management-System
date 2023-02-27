using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class ReportData : IReportData
{
    private readonly ISqlDataAccess _sql;

    public ReportData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<ReportModel> GetReports()
    {
        var output = _sql.LoadData<ReportModel, dynamic>("dbo.spReport_GetAll", new { }, "WSMData");

        return output;
    }

    public ReportModel GetReportById(ReportModel report)
    {
        var output = _sql.LoadData<ReportModel, dynamic>("dbo.spReport_GetById", new { report.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertReport(ReportModel report)
    {
        _sql.SaveData("dbo.spReport_Insert", report, "WSMData");
    }

    public void UpdateReport(ReportModel report)
    {
        _sql.SaveData("dbo.spReport_Update", new { report.Id, report.Title, report.Description, report.Archived }, "WSMData");
    }

    public void ArchiveReport(ReportModel report)
    {
        _sql.SaveData("dbo.spReport_Archive", new { report.Id, report.Archived }, "WSMData");
    }
}
