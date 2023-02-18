using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ITaskData
    {
        List<TaskModel> GetTaskByDepartmentId(int DepartmentId);
        List<TaskModel> GetTaskById(int Id);
        List<TaskModel> GetTaskByUserId(string UserId);
        List<TaskModel> GetTasks();
        void InsertTask(TaskModel task);
        void UpdatePercentage(TaskModel task);
        void UpdateTask(TaskModel task);
    }
}