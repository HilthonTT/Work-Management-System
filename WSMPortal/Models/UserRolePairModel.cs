using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class UserRolePairModel
{
    [Required(ErrorMessage = "A user must be selected.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "A role must be selected.")]
    public string RoleName { get; set; }
}
