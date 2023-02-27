using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IReportData
    {
        void ArchiveReport(ReportModel report);
        ReportModel GetReportById(ReportModel report);
        List<ReportModel> GetReports();
        void InsertReport(ReportModel report);
        void UpdateReport(ReportModel report);
    }
}