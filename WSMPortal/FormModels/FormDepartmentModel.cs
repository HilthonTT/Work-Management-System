using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.FormModels;

public class FormDepartmentModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Parent Company Is A Required Field.")]
    [DisplayName("Parent Company")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "Department Name Is A Required Field.")]
    [DisplayName("Department Name")]
    public string DepartmentName { get; set; }

    [Required(ErrorMessage = "Address Is A Required Field.")]
    public string Address { get; set; }
    public string ChairPersonId { get; set; }

    [Required(ErrorMessage = "Phone Number Is A Required Field.")]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Description Is A Required Field.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Created Date Is A Required Field.")]
    [DisplayName("Created Date")]
    [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = "DateTime Must Be Between 01/01/1753 And 31/12/9999")]
    public DateTime CreatedDate { get; set; }
}
