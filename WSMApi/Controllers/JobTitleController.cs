using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobTitleController : ControllerBase
{
    private readonly IJobTitleData _jobTitleData;

    public JobTitleController(IJobTitleData jobTitleData)
	{
        _jobTitleData = jobTitleData;
    }

    [HttpGet]
    [Authorize]
    [Route("GetJobTitles")]
    public List<JobTitleModel> GetJobTitles()
    {
        return _jobTitleData.GetJobTitles();
    }

    [HttpPost]
    [Authorize]
    [Route("GetJobTitlesById")]
    public JobTitleModel GetJobTitleById(JobTitleModel jobTitle)
    {
        return _jobTitleData.GetJobTitleById(jobTitle);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertJobTitle")]
    public void InsertJobTitle(JobTitleModel jobTitle)
    {
        _jobTitleData.InsertJobTitle(jobTitle);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateJobTitle")]
    public void UpdateJobTitle(JobTitleModel jobTitle)
    {
        _jobTitleData.UpdateJobTitle(jobTitle);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveJobTitle")]
    public void ArchiveJobTitle(JobTitleModel jobTitle)
    {
        _jobTitleData.ArchiveJobTitle(jobTitle);
    }
}
