using UI.Library.Models;

namespace UI.Library.API
{
    public interface ICompanyEndpoint
    {
        Task ArchiveCompanyAsync(CompanyModel company);
        Task<List<CompanyModel>> GetAllAsync();
        Task<CompanyModel> GetByIdAsync(int Id);
        Task PostCompanyAsync(CompanyModel company);
        Task UpdateCompanyAsync(CompanyModel company);
    }
}