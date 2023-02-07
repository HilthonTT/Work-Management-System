using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Library.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int DepartmentId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateDue { get; set; }
    public int PercentageDone { get; set; }
    public bool IsDone { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
