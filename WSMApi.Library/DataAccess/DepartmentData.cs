using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class DepartmentData : IDepartmentData
{
    private readonly ISqlDataAccess _sql;

    public DepartmentData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<DepartmentModel> GetDepartments()
    {
        var output = _sql.LoadData<DepartmentModel, dynamic>("dbo.spDepartment_GetAll", new { }, "WSMData");

        return output;
    }

    public DepartmentModel GetDepartmentById(DepartmentModel department)
    {
        var output = _sql.LoadData<DepartmentModel, dynamic>("dbo.spDepartment_GetById", new { department.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertDepartment(DepartmentModel department)
    {
        _sql.SaveData("dbo.spDepartment_Insert", department, "WSMData");
    }

    public void UpdateDepartment(DepartmentModel department)
    {
        _sql.SaveData("dbo.spDepartment_Update", department, "WSMData");
    }

    public void ArchiveDepartment(DepartmentModel department)
    {
        _sql.SaveData("dbo.spDepartment_Archive", new { department.Id, department.Archived }, "WSMData");
    }
}
