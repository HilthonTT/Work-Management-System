using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class ModifyPercentageTaskModel
{
    [Required]
    [Range(0, 100, ErrorMessage = "The Progress of the task must be greater than 1 and lower than 100.")]
    public int PercentageDone { get; set; }
}
