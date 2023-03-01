using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Library.Models;

public class CompanyModel
{
    /// <summary>
    /// The identifier for the CompanyModel
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the company
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// The address of the company
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The phone number of the company
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// The Id of the ChairPerson, it relates to the ID of the UserModel
    /// </summary>
    public string? ChairPersonId { get; set; }

    /// <summary>
    /// The description of the company, what they do, etc...
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The date it was founded
    /// </summary>
    public DateTime DateFounded { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; } = false;
}
