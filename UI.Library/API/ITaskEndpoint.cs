using UI.Library.Models;

namespace UI.Library.API
{
    public interface ITaskEndpoint
    {
        Task<List<TaskModel>> GetAll();
        Task<List<TaskModel>> GetByDepartmentId(string departmentId);
        Task<List<TaskModel>> GetByUserId();
        Task PostTask(TaskModel task);
        Task Update(TaskModel task);
        Task UpdatePercentage(TaskModel task);
    }
}