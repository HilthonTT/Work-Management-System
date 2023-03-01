namespace UI.Library.Models;

public class UserModel
{
    /// <summary>
    /// The Identifier for the User. It is a string because of Microsoft's auth system.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string EmailAddress { get; set; }

    /// <summary>
    /// The user's phone number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// The date the user was birth (IRL).
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// The department that the user is in. It relates to the DepartmentModel.
    /// </summary>
    public int? DepartmentId { get; set; }

    /// <summary>
    /// The Job Title that the user currently posssesses. It relates to the JobTileModel.
    /// </summary>
    public int? JobTitleId { get; set; }

    /// <summary>
    /// The date the user account was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


    /// <summary>
    /// The list of roles that the user has.
    /// </summary>

    public Dictionary<string, string> Roles { get; set; } = new();

    /// <summary>
    /// The list of job titles that the use has.
    /// </summary>
    public List<JobTitleModel> JobTitles { get; set; } = new();

    public string RoleList
    {
        get
        {
            return string.Join(", ", Roles.Select(x => x.Value));
        }
    }

    public string JobList
    {
        get
        {
            return string.Join(", ", JobTitles.Select(x => x.JobName));
        }
    }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}
