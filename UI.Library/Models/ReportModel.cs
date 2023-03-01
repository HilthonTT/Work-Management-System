namespace UI.Library.Models;

public class ReportModel
{
    /// <summary>
    /// The Identifier for the ReportModel
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Identifier for the task. Tells us which task this is relating to.
    /// </summary>
    public int TaskId { get; set; }

    /// <summary>
    /// The Identifier for the user. Tells us who created the report.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// The Title of the report.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The Description of the report.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The day it was created.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; } = false;
}
