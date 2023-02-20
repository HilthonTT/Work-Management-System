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
    public List<DepartmentModel> GetDepartments()
    {
        return _departmentData.GetDepartments();
    }

    [HttpPost]
    [Authorize]
    [Route("GetDepartmentById")] 
    public DepartmentModel GetDepartmentById(DepartmentModel department)
    {
        return _departmentData.GetDepartmentById(department);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertDepartment")]
    public void InsertDepartment(DepartmentModel department)
    {
        _departmentData.InsertDepartment(department);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateDepartment")]
    public void UpdateDepartment(DepartmentModel department) 
    { 
        _departmentData.UpdateDepartment(department);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveDepartment")]
    public void ArchiveDepartment(DepartmentModel department)
    {
        _departmentData.ArchiveDepartment(department);
    }

}
