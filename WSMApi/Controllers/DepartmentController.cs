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

    public record GetDepartmentName(
        string DepartmentName
    );

    [HttpPost]
    [Authorize]
    [Route("GetDepartmentByName")] 
    public List<DepartmentModel> GetDepartmentsByName(GetDepartmentName departmentName)
    {
        return _departmentData.GetDepartmentByName(departmentName.DepartmentName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("InsertDepartment")]
    public void InsertDepartment(DepartmentModel department)
    {
        _departmentData.InsertDepartment(department);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("UpdateDepartment")]
    public void UpdateDepartment(DepartmentModel department) 
    { 
        _departmentData.UpdateDepartment(department);
    }
}
