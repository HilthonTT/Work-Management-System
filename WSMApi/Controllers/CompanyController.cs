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

    public record GetCompanyName(
        string CompanyName
    );

    [HttpPost]
    [Authorize]
    [Route("GetCompanyByName")]
    public List<CompanyModel> GetCompanyByName(GetCompanyName companyName)
    {
        return _companyData.GetCompanyByName(companyName.CompanyName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("InsertCompany")]
    public void InsertCompany(CompanyModel company)
    {
        _companyData.InsertCompany(company);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("UpdateCompany")]
    public void UpdateCompany(CompanyModel company)
    {
        _companyData.UpdateCompany(company);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("DeleteCompany")]
    public void DeleteCompany(CompanyModel company)
    {
        _companyData.DeleteCompany(company);
    }
}
