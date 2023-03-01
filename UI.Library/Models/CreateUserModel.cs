using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UI.Library.Models;

public class CreateUserModel
{
    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required]
    [DisplayName("Email Address")]
    public string EmailAddress { get; set; }

    [Required]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    [Required]
    [DisplayName("Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    public int? DepartmentId { get; set; } = null;

    public int? JobTitleId { get; set; } = null;

    [Required]
    [MinLength(6, ErrorMessage = "Password Must Be At Least 6 Characters Long.")]
    public string Password { get; set; }

    [Required]
    [DisplayName("Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "The Passwords Do Not Match.")]
    public string ConfirmPassword { get; set; }
}
