using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class CreateTaskModel
{

    [Required]
    [DisplayName("User")]
    public string? UserId { get; set; }

    [Required]
    [DisplayName("Department")]
    public int? DepartmentId { get; set; }

    [Required]
    [MaxLength(75)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    [DisplayName("Date Due")]
    [Range(typeof(DateTime), "1/1/1900", "1/1/9999",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public DateTime DateDue { get; set; }

    public int PercentageDone { get; set; }
    public bool IsDone { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public bool Archived { get; set; }
}
