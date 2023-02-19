using UI.Library.Models;

namespace UI.Library.API
{
    public interface ITaskEndpoint
    {
        Task ArchiveTask(TaskModel task);
        Task<List<TaskModel>> GetAllAsync();
        Task<List<TaskModel>> GetByDepartmentIdAsync(TaskModel task);
        Task<List<TaskModel>> GetByUserIdAsync(TaskModel task);
        Task<TaskModel> GetTaskById(TaskModel task);
        Task PostTaskAsync(TaskModel task);
        Task UpdateAsync(TaskModel task);
        Task UpdatePercentageAsync(TaskModel task);
    }
}