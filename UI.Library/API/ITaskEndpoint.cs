using UI.Library.Models;

namespace UI.Library.API
{
    public interface ITaskEndpoint
    {
        Task<List<TaskModel>> GetAllAsync();
        Task<List<TaskModel>> GetByDepartmentIdAsync(string departmentId);
        Task<List<TaskModel>> GetByUserIdAsync(string userId);
        Task<TaskModel> GetTaskById(int Id);
        Task PostTaskAsync(TaskModel task);
        Task UpdateAsync(TaskModel task);
        Task UpdatePercentageAsync(TaskModel task);
    }
}