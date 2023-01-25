using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMDesktop.Models;

public class TaskDisplayModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int DepartmentId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateDue { get; set; }
    public int PercentageDone { get; set; }
    public bool IsDone { get; set; }
    public DateTime DateCreated { get; set; }

    public string FormattedDateDue()
    {
        return DateDue.ToString("dd/MM/yyyy");
    }

    public string DisplayText
    {
        get
        {
            return $"{Title} dued {FormattedDateDue()} -- {PercentageDone}%";
        }
    }
}
