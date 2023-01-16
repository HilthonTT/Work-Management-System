using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ICompanyData
    {
        List<CompanyModel> GetCompanies();
        List<CompanyModel> GetCompanyByName(string CompanyName);
        void InsertCompany(CompanyModel company);
        void UpdateUser(CompanyModel company);
    }
}