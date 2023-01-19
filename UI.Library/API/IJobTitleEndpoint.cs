using UI.Library.Models;

namespace UI.Library.API
{
    public interface IJobTitleEndpoint
    {
        Task<List<JobTitleModel>> GetAll();
        Task<List<JobTitleModel>> GetByName(string JobName);
        Task PostJobTitle(JobTitleModel JobTitle);
        Task UpdateJobTitle(JobTitleModel JobTitle);
    }
}