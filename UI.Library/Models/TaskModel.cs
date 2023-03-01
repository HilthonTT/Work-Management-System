namespace UI.Library.Models;

public class TaskModel
{
    /// <summary>
    /// The Identifier for the task
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Identifier for the user. Tells us which user the task was assigned to.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The Identifier for the department. Tells us which department the task was assigned to.
    /// </summary>
    public int? DepartmentId { get; set; }

    /// <summary>
    /// The title of the task
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Its description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The date the task is dued. When it has to be completed.
    /// </summary>
    public DateTime DateDue { get; set; }

    /// <summary>
    /// The progress of the task. On a scale of 1 to 100%
    /// </summary>
    public int PercentageDone { get; set; }

    /// <summary>
    /// If the task is 100%. It will display as done.
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    /// The date the task was created.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; } = false;
}
