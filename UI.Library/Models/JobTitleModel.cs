namespace UI.Library.Models;

public class JobTitleModel
{
    /// <summary>
    /// The Identifier for the ItemModel.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Job Title Name
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// Its description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The Department the job title is related to.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; } = false;
}
