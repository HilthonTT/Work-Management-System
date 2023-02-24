using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class ItemModel
{
    public int Id { get; set; }
    public string ModelName { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public string? InternalSupplierPersonId { get; set; }
    public int? InternalSupplierCompanyId { get; set; }
    public decimal? EAN { get; set; }
    public bool Archived { get; set; }
}
