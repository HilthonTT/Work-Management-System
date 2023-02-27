namespace UI.Library.Models;

public class ReportModel
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public bool Archived { get; set; } = false;
}
