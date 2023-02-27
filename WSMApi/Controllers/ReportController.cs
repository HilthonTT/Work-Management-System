using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportData _reportData;

    public ReportController(IReportData reportData)
	{
        _reportData = reportData;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("Admin/GetReports")]
    public List<ReportModel> GetReports()
    {
        return _reportData.GetReports();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/GetReportById")]
    public ReportModel GetReportById(ReportModel report)
    {
        return _reportData.GetReportById(report);
    }

    [HttpPost]
    [Authorize]
    [Route("InsertReport")]
    public void InsertReport(ReportModel report)
    {
        _reportData.InsertReport(report);
    }

    [HttpPut]
    [Authorize]
    [Route("UpdateReport")]
    public void Updatereport(ReportModel report)
    {
        _reportData.UpdateReport(report);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveReport")]
    public void ArchiveReport(ReportModel report)
    {
        _reportData.ArchiveReport(report);
    }
}
