﻿using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    [Authorize]
    [Route("GetJobTitlesByName")]
    public List<JobTitleModel> GetJobTitleByName(string JobName)
    {
        return _jobTitleData.GetJobTitleByName(JobName);
    }

    [HttpPost]
    [Authorize]
    [Route("InsertJobTitle")]
    public void InsertJobTitle(JobTitleModel jobTitle)
    {
        _jobTitleData.InsertJobTitle(jobTitle);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateJobTitle")]
    public void UpdateJobTitle(JobTitleModel jobTitle)
    {
        _jobTitleData.UpdateJobTitle(jobTitle);
    }
}