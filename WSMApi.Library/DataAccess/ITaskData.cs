using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ITaskData
    {
        void ArchiveTask(TaskModel task);
        List<TaskModel> GetTaskByDepartmentId(TaskModel task);
        TaskModel GetTaskById(TaskModel task);
        List<TaskModel> GetTaskByUserId(TaskModel task);
        List<TaskModel> GetTasks();
        void InsertTask(TaskModel task);
        void UpdatePercentage(TaskModel task);
        void UpdateTask(TaskModel task);
    }
}