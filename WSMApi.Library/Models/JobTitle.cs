using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class JobTitle
{
    public int Id { get; set; }
    public string JobName { get; set; }
    public string Description { get; set; }
    public int DepartmentId { get; set; }
}
