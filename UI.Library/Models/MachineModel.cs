using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Library.Models;

public class MachineModel
{
    public int Id { get; set; }
    public string MachineName { get; set; }
    public string ModelName { get; set; }
    public string EuropeanArticleNumber { get; set; }
    public DateTime DatePurchased { get; set; }
}
