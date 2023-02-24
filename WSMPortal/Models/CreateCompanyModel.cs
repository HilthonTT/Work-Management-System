using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class CreateCompanyModel
{
    public int Id { get; set; }

    [Required]
    [DisplayName("Company Name")]
    public string CompanyName { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    [DisplayName("User")]
    public string ChairPersonId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime DateFounded { get; set; }
    public bool Archived { get; set; }
}
