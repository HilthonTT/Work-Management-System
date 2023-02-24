using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WSMPortal.Models;

public class CreateDepartmentModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Company is a required field.")]
    [DisplayName("Company")]
    public int? CompanyId { get; set; }

    [Required(ErrorMessage = "The Department Name is a required field.")]
    [DisplayName("Department Name")]
    public string DepartmentName { get; set; }

    [Required(ErrorMessage = "The Address is a required field.")]
    public string Address { get; set; }

    [DisplayName("ChairPerson")]
    public string? ChairPersonId { get; set; }

    [Required(ErrorMessage = "Phone Number is a required field.")]
    [DisplayName("Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The Description is a required field.")]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    [DisplayName("Created Date")]
    public DateTime CreatedDate { get; set; }
    public bool Archived { get; set; }
}
