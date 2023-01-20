using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface ITaskData
    {
        List<TaskModel> GetTaskByDepartmentId(string DepartmentId);
        List<TaskModel> GetTaskByUserId(string UserId);
        List<TaskModel> GetTasks();
        void InsertTask(TaskModel task);
        void UpdatePercentage(int percentage);
        void UpdateTask(TaskModel task);
    }
}