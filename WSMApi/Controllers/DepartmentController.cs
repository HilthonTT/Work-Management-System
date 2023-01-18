using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentData _departmentData;

    public DepartmentController(IDepartmentData departmentData)
	{
        _departmentData = departmentData;
    }

    [HttpGet]
    [Authorize]
    [Route("GetDepartments")]
    public List<DepartmentModel> Get()
    {
        return _departmentData.GetDepartments();
    }

    [HttpGet]
    [Authorize]
    [Route("GetDepartmentByName")] 
    public List<DepartmentModel> GetDepartments(string DepartmentName)
    {
        return _departmentData.GetDepartmentByName(DepartmentName);
    }

    [HttpPost]
    [Authorize]
    [Route("InsertDepartment")]
    public void InsertDepartment(DepartmentModel department)
    {
        _departmentData.InsertDepartment(department);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateDepartment")]
    public void UpdateDepartment(DepartmentModel department) 
    { 
        _departmentData.UpdateDepartment(department);
    }
}
