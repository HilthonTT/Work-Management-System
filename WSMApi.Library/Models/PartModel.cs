using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class PartModel
{
    public int Id { get; set; }
    public string PartName { get; set; }
    public string ModelName { get; set; }
    public decimal PurchasedPrice { get; set; }
    public int? MachineId { get; set; }
    public DateTime DatePurchased { get; set; }
}
