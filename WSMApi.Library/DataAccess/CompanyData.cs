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

    public CompanyModel GetCompanyById(CompanyModel company)
    {
        var output = _sql.LoadData<CompanyModel, dynamic>("dbo.spCompany_GetById", new { company.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Insert", company, "WSMData");
    }

    public void UpdateCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Update", company, "WSMData");
    }

    public void ArchiveCompany(CompanyModel company)
    {
        _sql.SaveData("dbo.spCompany_Archive", new { company.Id, company.Archived }, "WSMData");
    }
}
