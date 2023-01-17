using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IDepartmentData
    {
        List<DepartmentModel> GetDepartmentByName(string DepartmentName);
        List<DepartmentModel> GetDepartments();
        void InsertDepartment(DepartmentModel department);
        void UpdateDepartment(DepartmentModel department);
    }
}