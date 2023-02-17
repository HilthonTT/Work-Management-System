using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.FormModels;

public class FormCompanyModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Company Name Is A Required Field.")]
    [DisplayName("Company Name")]
    public string CompanyName { get; set; }

    [Required(ErrorMessage = "The Address Is A Required Field.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "The Phone Number Is A Required Field.")]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }
    public string ChairPersonId { get; set; }

    [Required(ErrorMessage = "The Description Is A Required Field.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Date Founded Is A Required Field.")]
    [DisplayName("Date Founded")]
    [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = "DateTime Must Be Between 01/01/1753 And 31/12/9999")]
    public DateTime DateFounded { get; set; }
}
