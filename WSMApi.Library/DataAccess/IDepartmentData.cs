using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IDepartmentData
    {
        void ArchiveDepartment(DepartmentModel department);
        DepartmentModel GetDepartmentById(DepartmentModel department);
        List<DepartmentModel> GetDepartments();
        void InsertDepartment(DepartmentModel department);
        void UpdateDepartment(DepartmentModel department);
    }
}