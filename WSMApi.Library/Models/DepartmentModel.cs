namespace WSMApi.Library.Models;

public class DepartmentModel
{
    /// <summary>
    /// The Identifier for the DepartmentModel.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Identifier for the company it is related. It relates to the CompanyModel. Tells who is the Parent Company.
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// The name of the department.
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// The address of the department.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The Identifier for the user it is related. It relates to the UserModel. Tells who is the current Chair Person.
    /// </summary>
    public string? ChairPersonId { get; set; }

    /// <summary>
    /// The phone number of the company
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// The description of the company
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The date of it was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; } = false;
}
