using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class UserJobPairModel
{
    [Required(ErrorMessage = "A User Must Be Selected.")]
    public string Id { get; set; }
    [Required(ErrorMessage = "A Job Must Be Selected.")]
    public int JobId { get; set; }
}
