using UI.Library.Models;

namespace UI.Library.API
{
    public interface IJobTitleEndpoint
    {
        Task ArchiveJobTitleAsync(JobTitleModel jobTitle);
        Task<List<JobTitleModel>> GetAllAsync();
        Task<JobTitleModel> GetByIdAsync(int Id);
        Task PostJobTitleAsync(JobTitleModel JobTitle);
        Task UpdateJobTitleAsync(JobTitleModel JobTitle);
    }
}