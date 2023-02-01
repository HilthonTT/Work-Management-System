using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMDesktop.Models;

public class MachineDisplayModel
{
    public int Id { get; set; }
    public string MachineName { get; set; }
    public string ModelName { get; set; }
    public decimal PurchasedPrice { get; set; }
    public string EuropeanArticleNumber { get; set; }
    public DateTime DatePurchased { get; set; }

    public string DisplayText
    {
        get
        {
            return $"{MachineName} - {ModelName}";
        }
    }
}
