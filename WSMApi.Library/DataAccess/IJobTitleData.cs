using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IJobTitleData
    {
        List<JobTitleModel> GetJobTitleByName(string JobName);
        List<JobTitleModel> GetJobTitles();
        void InsertJobTitle(JobTitleModel jobTitle);
        void UpdateJobTitle(JobTitleModel jobTitle);
    }
}