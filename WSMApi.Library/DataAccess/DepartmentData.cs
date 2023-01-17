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

    public List<DepartmentModel> GetDepartmentByName(string DepartmentName)
    {
        var output = _sql.LoadData<DepartmentModel, dynamic>("dbo.spDepartment_GetByName", new { DepartmentName }, "WSMData");

        return output;
    }


    public void InsertDepartment(DepartmentModel department)
    {
        _sql.SaveData("dbo.spDepartment_Insert", department, "WSMData");
    }

    public void UpdateDepartment(DepartmentModel department)
    {
        _sql.SaveData("dbo.spDepartment_Update", department, "WSMData");
    }
}
