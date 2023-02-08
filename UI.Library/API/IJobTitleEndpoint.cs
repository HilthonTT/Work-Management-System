using UI.Library.Models;

namespace UI.Library.API
{
    public interface IJobTitleEndpoint
    {
        Task<List<JobTitleModel>> GetAllAsync();
        Task<List<JobTitleModel>> GetByNameAsync(string JobName);
        Task PostJobTitleAsync(JobTitleModel JobTitle);
        Task UpdateJobTitleAsync(JobTitleModel JobTitle);
    }
}