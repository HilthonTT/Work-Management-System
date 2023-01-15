using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class Department
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string DepartmentName { get; set; }
    public string ChairPersonId { get; set; }
    public decimal Budget { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
