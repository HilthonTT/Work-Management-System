using UI.Library.Models;

namespace UI.Library.API
{
    public interface IReportEndpoint
    {
        Task ArchiveReportAsync(ReportModel report);
        Task<List<ReportModel>> GetAllAsync();
        Task<ReportModel> GetByIdAsync(int Id);
        Task InsertReportAsync(ReportModel report);
        Task UpdateReportAsync(ReportModel report);
    }
}