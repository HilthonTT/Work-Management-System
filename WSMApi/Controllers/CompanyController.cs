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
    public List<CompanyModel> Get()
    {
        return _companyData.GetCompanies();
    }

    [HttpGet]
    [Authorize]
    [Route("GetCompanyByName")]
    public List<CompanyModel> GetCompanyByName(string CompanyName)
    {
        return _companyData.GetCompanyByName(CompanyName);
    }

    [HttpPost]
    [Authorize]
    [Route("InsertCompany")]
    public void InsertCompany(CompanyModel company)
    {
        _companyData.InsertCompany(company);
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateCompany")]
    public void UpdateCompany(CompanyModel company)
    {
        _companyData.UpdateCompany(company);
    }
}
