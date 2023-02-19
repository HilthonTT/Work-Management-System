using UI.Library.Models;

namespace UI.Library.API
{
    public interface IDepartmentEndpoint
    {
        Task ArchiveDepartmentAsync(DepartmentModel department);
        Task<List<DepartmentModel>> GetAllAsync();
        Task<DepartmentModel> GetByIdAsync(int Id);
        Task PostDepartmentAsync(DepartmentModel department);
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}