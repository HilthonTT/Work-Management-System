using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyData _companyData;

    public CompanyController(ICompanyData companyData)
	{
        _companyData = companyData;
    }

    [HttpGet]
    [Authorize]
    [Route("GetCompanies")]
    public List<CompanyModel> GetCompanies()
    {
        return _companyData.GetCompanies();
    }

    [HttpPost]
    [Authorize]
    [Route("GetCompanyById")]
    public CompanyModel GetCompanyById(CompanyModel company)
    {
        return _companyData.GetCompanyById(company);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertCompany")]
    public void InsertCompany(CompanyModel company)
    {
        _companyData.InsertCompany(company);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateCompany")]
    public void UpdateCompany(CompanyModel company)
    {
        _companyData.UpdateCompany(company);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveCompany")]
    public void ArchiveCompany(CompanyModel company)
    {
        _companyData.ArchiveCompany(company);
    }
}
