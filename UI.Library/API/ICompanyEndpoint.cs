using UI.Library.Models;

namespace UI.Library.API
{
    public interface ICompanyEndpoint
    {
        Task DeleteCompanyAsync(CompanyModel company);
        Task<List<CompanyModel>> GetAllAsync();
        Task<List<CompanyModel>> GetByNameAsync(string CompanyName);
        Task PostCompanyAsync(CompanyModel company);
        Task UpdateCompanyAsync(CompanyModel company);
    }
}