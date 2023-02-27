using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class CreateReportModel
{
    public int Id { get; set; }

    [Required]
    [DisplayName("Task")]
    public int? TaskId { get; set; }

    [DisplayName("Author")]
    public string UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public bool Archived { get; set; } = false;
}
