using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IJobTitleData
    {
        void ArchiveJobTitle(JobTitleModel jobTitle);
        JobTitleModel GetJobTitleById(JobTitleModel jobTitle);
        List<JobTitleModel> GetJobTitles();
        void InsertJobTitle(JobTitleModel jobTitle);
        void UpdateJobTitle(JobTitleModel jobTitle);
    }
}