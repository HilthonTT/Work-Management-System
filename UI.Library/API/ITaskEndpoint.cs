using UI.Library.Models;

namespace UI.Library.API
{
    public interface ITaskEndpoint
    {
        Task ArchiveTask(TaskModel task);
        Task<List<TaskModel>> GetAllAsync();
        Task<List<TaskModel>> GetByDepartmentIdAsync(int departmentId);
        Task<List<TaskModel>> GetByUserIdAsync(int Id);
        Task<TaskModel> GetTaskById(int Id);
        Task PostTaskAsync(TaskModel task);
        Task UpdateAsync(TaskModel task);
        Task UpdatePercentageAsync(TaskModel task);
    }
}