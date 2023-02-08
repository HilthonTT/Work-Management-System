using UI.Library.Models;

namespace UI.Library.API
{
    public interface IDepartmentEndpoint
    {
        Task DeleteDepartmentAsync(DepartmentModel department);
        Task<List<DepartmentModel>> GetAllAsync();
        Task<List<DepartmentModel>> GetByNameAsync(string DepartmentName);
        Task PostDepartmentAsync(DepartmentModel department);
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}