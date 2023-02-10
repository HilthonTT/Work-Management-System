using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class CreateTaskModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int? DepartmentId { get; set; }

    [Required(ErrorMessage = "Title is a required field.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is a required field.")]
    public string Description { get; set; }

    [Required]
    [DisplayName("Date Due")]
    [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = "DateTime must be between 01/01/1753 and 31/12/9999")]
    public DateTime DateDue { get; set; } = DateTime.UtcNow;
    public int PercentageDone { get; set; }
    public bool IsDone { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
