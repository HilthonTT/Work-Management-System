using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMDesktop.Models;

public class PartDisplayModel
{
    public int Id { get; set; }
    public string PartName { get; set; }
    public string ModelName { get; set; }
    public int? MachineId { get; set; }
    public DateTime DatePurchased { get; set; }

    public string DisplayText
    {
        get
        {
            return $"{PartName} - {ModelName}";
        }
    }
}
