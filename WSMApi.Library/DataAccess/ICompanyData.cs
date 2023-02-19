using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ICompanyData
    {
        void ArchiveCompany(CompanyModel company);
        List<CompanyModel> GetCompanies();
        CompanyModel GetCompanyById(CompanyModel company);
        void InsertCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
    }
}