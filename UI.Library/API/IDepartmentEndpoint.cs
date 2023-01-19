using UI.Library.Models;

namespace UI.Library.API
{
    public interface IDepartmentEndpoint
    {
        Task<List<DepartmentModel>> GetAll();
        Task<List<DepartmentModel>> GetByName(string DepartmentName);
        Task PostDepartment(DepartmentModel department);
        Task UpdateDepartment(DepartmentModel department);
    }
}