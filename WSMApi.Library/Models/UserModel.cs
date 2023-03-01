namespace WSMApi.Library.Models;

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
}
