using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class CompanyData : ICompanyData
{
    private readonly ISqlDataAccess _sql;

    public CompanyData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public List<CompanyModel> GetCompanies()
    {
        var output = _sql.LoadData<CompanyModel, dynamic>("dbo.spCompany_GetAll", new { }, "WSMData");

        return output;
    }

    public List<CompanyModel> GetCompanyByName(string CompanyName)
    {
        var output = _sql.LoadData<CompanyModel, dynamic>("dbo.spCompany_GetByName", new { CompanyName }, "WSMData");

        return output;
    }

    public void InsertCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Insert", company, "WSMData");
    }

    public void UpdateCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Update", company, "WSMData");
    }

    public void DeleteCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Delete", new { company.Id }, "WSMData");
    }
}
