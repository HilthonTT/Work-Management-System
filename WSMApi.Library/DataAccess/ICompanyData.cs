using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ICompanyData
    {
        void DeleteCompany(CompanyModel company);
        List<CompanyModel> GetCompanies();
        List<CompanyModel> GetCompanyByName(string CompanyName);
        void InsertCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
    }
}